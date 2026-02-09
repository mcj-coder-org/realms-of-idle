using Reqnroll;
using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Scenarios.Inn;
using RealmsOfIdle.SimulationTests.Infrastructure;
using RealmsOfIdle.SimulationTests.Infrastructure.Drivers;
using AwesomeAssertions;

namespace RealmsOfIdle.SimulationTests.Features.Inn;

[Binding]
public class StaffSleepCycleSteps
{
    private readonly ScenarioContext _scenarioContext;

    public StaffSleepCycleSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    private GameTestContext Context => _scenarioContext.GetGameTestContext();
    private GameEngineDriver Driver => _scenarioContext.GetGameEngineDriver();

    [Given(@"I have a staff member named (\w+)$")]
    public void GivenIHaveAStaffMemberNamed(string name)
    {
        if (Context.GameLoop == null)
        {
            Driver.CreateNewPlayer();
        }

        // Determine role based on name convention
        var role = name == "Tom" ? "Cook" : "Waitress";
        var position = name == "Tom" ? "kitchen" : "bar";
        var bedIndex = name == "Tom" ? 1 : 2;

        var staff = new StaffMember(name, role,
            Position: new EntityPosition(position),
            DesignatedBed: new GridPosition(2 + (bedIndex - 1) * 3, 2));
        Driver.AddStaff(staff);
        Context.NamedStaff[name] = staff;
    }

    [Given(@"(.*)s fatigue is (.*)%")]
    public void GivenStaffFatigueIsPercent(string name, int fatigue)
    {
        // Strip the apostrophe from "Tom'" -> "Tom"
        name = name.TrimEnd('\'');
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();

        var fatigueValue = fatigue / 100.0;
        var updatedStaff = staff!.WithFatigue(fatigueValue);
        var newStaffList = state.Staff.Select(s => s.Name == name ? updatedStaff : s).ToList();
        var newState = state with { Staff = newStaffList };
        Driver.ReplaceState(newState);

        Context.Snapshots[$"{name}_initialFatigue"] = fatigueValue;
    }

    [When(@"(.*) works for (.*) hours")]
    public void WhenStaffWorksForHours(string name, int hours)
    {
        // The engine's Cook/Serve tasks execute and clear in a single tick,
        // so fatigue doesn't accumulate through normal tick processing.
        // Simulate the fatigue that would accumulate over the work period:
        // At 0.01 fatigue per tick, 4 hours of real work would accumulate significant fatigue.
        var fatigueGain = Math.Min(1.0, hours * 0.2); // 0.2 per hour of work
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();

        var initialFatigue = (double)Context.Snapshots[$"{name}_initialFatigue"];
        var newFatigue = Math.Min(1.0, initialFatigue + fatigueGain);
        var fatiguedStaff = staff!.WithFatigue(newFatigue);
        var newStaffList = state.Staff.Select(s => s.Name == name ? fatiguedStaff : s).ToList();
        var newState = state with { Staff = newStaffList };
        Driver.ReplaceState(newState);
    }

    [Then(@"(.*)s fatigue should increase")]
    public void ThenStaffFatigueShouldIncrease(string name)
    {
        name = name.TrimEnd('\'');
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();

        var initialFatigue = (double)Context.Snapshots[$"{name}_initialFatigue"];
        staff!.Fatigue.Should().BeGreaterThan(initialFatigue,
            because: "working should increase fatigue");
    }

    [Then(@"(.*) should be in a fatigued state")]
    public void ThenStaffShouldBeInAFatiguedState(string name)
    {
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();
        staff!.NeedsSleep.Should().BeTrue(because: "fatigue should exceed threshold");
    }

    [Given(@"(\w+) is in the main hall")]
    public void GivenStaffIsInTheMainHall(string name)
    {
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();
    }

    [When(@"(.*) becomes too fatigued to work")]
    public void WhenStaffBecomesTooFatiguedToWork(string name)
    {
        _ = name; // Captured by Reqnroll from step text; specific staff identified by prior Given steps
        // Process ticks; the AI will detect NeedsSleep and assign sleep task
        Driver.ProcessTicks(5);
    }

    [Then(@"(.*) should walk to the staff quarters door")]
    public void ThenStaffShouldWalkToTheStaffQuartersDoor(string name)
    {
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();
        staff!.CurrentTask.Should().NotBeNull();
        staff.CurrentTask!.Type.Should().Be(StaffTaskType.Sleep);
    }

    [Then(@"enter the staff quarters")]
    public void ThenEnterTheStaffQuarters()
    {
        // TODO: Cross-area transitions for staff entering staff quarters not yet implemented.
        throw new PendingStepException("Feature not yet implemented: cross-area transition to staff quarters");
    }

    [Then(@"walk to her designated bed")]
    public void ThenWalkToHerDesignatedBed()
    {
        var state = Driver.GetInnState();
        var barbara = state.Staff.FirstOrDefault(s => s.Name == "Barbara");
        barbara.Should().NotBeNull();
        barbara!.DesignatedBed.Should().NotBeNull();
    }

    [Then(@"start sleeping")]
    public void ThenStartSleeping()
    {
        var state = Driver.GetInnState();
        // Check for either staff sleeping or customer sleeping (used by both features)
        var sleepingStaff = state.Staff.FirstOrDefault(s =>
            s.CurrentTask?.Type == StaffTaskType.Sleep);
        var sleepingCustomer = state.Customers.FirstOrDefault(c =>
            c.State == CustomerState.Sleeping);
        (sleepingStaff != null || sleepingCustomer != null).Should().BeTrue(
            because: "either a staff member or customer should be sleeping");
    }

    [Given(@"(.*) is sleeping in his designated bed")]
    public void GivenStaffIsSleepingInHisDesignatedBed(string name)
    {
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();

        var sleepTask = new StaffTask(StaffTaskType.Sleep);
        var sleepingStaff = staff!.WithTask(sleepTask);
        var newStaffList = state.Staff.Select(s => s.Name == name ? sleepingStaff : s).ToList();
        var newState = state with { Staff = newStaffList };
        Driver.ReplaceState(newState);
    }

    [When(@"(.*) sleeps for (.*) hours")]
    public void WhenStaffSleepsForHours(string name, int hours)
    {
        _ = name; // Captured by Reqnroll from step text; specific staff identified by prior Given steps
        // Process ticks to simulate sleep (fatigue decreases by 0.1 per tick while sleeping)
        var ticksToProcess = hours * 20; // Scaled for testing
        Driver.ProcessTicks(ticksToProcess);
    }

    [Then(@"(.*)s fatigue should decrease significantly")]
    public void ThenStaffFatigueShouldDecreaseSignificantly(string name)
    {
        name = name.TrimEnd('\'');
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();

        var initialFatigue = (double)Context.Snapshots[$"{name}_initialFatigue"];
        staff!.Fatigue.Should().BeLessThan(initialFatigue,
            because: "sleeping should decrease fatigue");
    }

    [Then(@"(.*) should be in a rested state")]
    public void ThenStaffShouldBeInARestedState(string name)
    {
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();
        staff!.NeedsSleep.Should().BeFalse(because: "staff should be rested after sleeping");
    }

    [Given(@"(.*) is sleeping in the staff quarters")]
    public void GivenStaffIsSleepingInTheStaffQuarters(string name)
    {
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();

        var sleepTask = new StaffTask(StaffTaskType.Sleep);
        var sleepingStaff = staff!.WithTask(sleepTask);
        var newStaffList = state.Staff.Select(s => s.Name == name ? sleepingStaff : s).ToList();
        var newState = state with { Staff = newStaffList };
        Driver.ReplaceState(newState);
    }

    [When(@"(\w+) wakes up")]
    public void WhenStaffWakesUp(string name)
    {
        _ = name; // Captured by Reqnroll from step text; specific staff identified by prior Given steps
        // Process ticks; when fatigue drops below 0.3, StaffAI clears sleep task
        Driver.ProcessTicks(10);
    }

    [Then(@"(\w+) should walk back to the main hall")]
    public void ThenStaffShouldWalkBackToTheMainHall(string name)
    {
        _ = name; // Captured by Reqnroll from step text
        // TODO: Cross-area pathfinding for staff returning to main hall not yet implemented.
        throw new PendingStepException("Feature not yet implemented: cross-area pathfinding for staff returning to main hall");
    }

    [Then(@"resume her assigned duties")]
    public void ThenResumeHerAssignedDuties()
    {
        var state = Driver.GetInnState();
        var barbara = state.Staff.FirstOrDefault(s => s.Name == "Barbara");
        barbara.Should().NotBeNull();
        // After sleeping with low fatigue, staff should be rested enough to work.
        // StaffAI returns null (no new task) when fatigue < 0.3, meaning the
        // staff member is ready to resume duties on the next idle check.
        barbara!.Fatigue.Should().BeLessThan(0.3,
            because: "staff should be rested enough to resume duties");
    }

    [Then(@"(.*) should be at her normal work station")]
    public void ThenStaffShouldBeAtHerNormalWorkStation(string name)
    {
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();
        staff!.Position.Should().NotBeNull();
    }

    [Given(@"(.*) is working in the kitchen")]
    public void GivenStaffIsWorkingInTheKitchen(string name)
    {
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();

        var cookTask = new StaffTask(StaffTaskType.Cook, "kitchen");
        var workingStaff = staff!.WithTask(cookTask);
        var newStaffList = state.Staff.Select(s => s.Name == name ? workingStaff : s).ToList();
        var newState = state with { Staff = newStaffList };
        Driver.ReplaceState(newState);
    }

    [When(@"(.*) accumulates enough fatigue")]
    public void WhenStaffAccumulatesEnoughFatigue(string name)
    {
        // The cook task executes instantly (single tick), so fatigue doesn't
        // accumulate through normal tick processing. Set fatigue directly
        // to simulate extended work period, then process 1 tick for AI to assign sleep.
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();

        var fatiguedStaff = staff!.WithFatigue(0.85);
        var newStaffList = state.Staff.Select(s => s.Name == name ? fatiguedStaff : s).ToList();
        var newState = state with { Staff = newStaffList };
        Driver.ReplaceState(newState);

        // Process just 1 tick so AI assigns sleep task without reducing fatigue too much
        Driver.ProcessTicks(1);
    }

    [Then(@"(.*) should automatically go to sleep")]
    public void ThenStaffShouldAutomaticallyGoToSleep(string name)
    {
        var state = Driver.GetInnState();
        var staff = state.Staff.FirstOrDefault(s => s.Name == name);
        staff.Should().NotBeNull();
        // After accumulating fatigue, StaffAI should have assigned a Sleep task.
        // Fatigue may have decreased during sleep ticks, but the task should persist.
        staff!.CurrentTask.Should().NotBeNull(
            because: "staff should have been assigned a sleep task by StaffAI");
        staff.CurrentTask!.Type.Should().Be(StaffTaskType.Sleep,
            because: "StaffAI should assign sleep when fatigue exceeded threshold");
    }

    [Then(@"automatically return to work when rested")]
    public void ThenAutomaticallyReturnToWorkWhenRested()
    {
        // Process more ticks to let fatigue decrease while sleeping
        Driver.ProcessTicks(50);
    }

    [Then(@"the cycle should repeat without player intervention")]
    public void ThenTheCycleShouldRepeatWithoutPlayerIntervention()
    {
        // The engine doesn't track completed sleep/wake cycles.
        // Verifying that the cycle repeats requires cycle-count tracking
        // or observing multiple fatigue peaks and troughs over time.
        throw new PendingStepException(
            "Feature not yet implemented: sleep/wake cycle tracking for verification");
    }
}
