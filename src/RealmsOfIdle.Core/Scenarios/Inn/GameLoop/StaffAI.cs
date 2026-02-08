namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Handles AI decision making for staff members
/// </summary>
public static class StaffAI
{
    /// <summary>
    /// Decides the next action for a staff member
    /// </summary>
    public static StaffTask? DecideAction(StaffMember staff, InnState state)
    {
        ArgumentNullException.ThrowIfNull(staff);
        ArgumentNullException.ThrowIfNull(state);

        // If currently sleeping, continue sleeping
        if (staff.CurrentTask?.Type == StaffTaskType.Sleep)
        {
            // Check if rested enough
            if (staff.Fatigue < 0.3)
            {
                return null; // Done sleeping, will return to work
            }
            return new StaffTask(StaffTaskType.Sleep);
        }

        // If tired, go to sleep
        if (staff.NeedsSleep && staff.DesignatedBed != null)
        {
            return new StaffTask(StaffTaskType.Sleep);
        }

        // Role-specific AI
        return staff.Role switch
        {
            "Cook" => DecideCookAction(staff, state),
            "Waitress" or "Waiter" => DecideWaiterAction(staff, state),
            "Bartender" => new StaffTask(StaffTaskType.TendBar, "bar"),
            _ => null
        };
    }

    private static StaffTask? DecideCookAction(StaffMember staff, InnState _)
    {
        // Cook always cooks when idle
        if (staff.IsIdle)
        {
            return new StaffTask(StaffTaskType.Cook, "kitchen");
        }
        return null;
    }

    private static StaffTask? DecideWaiterAction(StaffMember staff, InnState state)
    {
        if (!staff.IsIdle)
        {
            return null;
        }

        // Check for customers waiting to be seated
        var waitingCustomer = state.Customers.FirstOrDefault(c => c.State == CustomerState.Waiting);
        if (waitingCustomer != null)
        {
            return new StaffTask(StaffTaskType.SeatGuest, waitingCustomer.Name);
        }

        // Check for seated customers waiting to order
        var needsOrder = state.Customers.FirstOrDefault(c =>
            c.State == CustomerState.Seated && c.Order == null);
        if (needsOrder != null)
        {
            return new StaffTask(StaffTaskType.Serve, needsOrder.Name);
        }

        // Check for customers waiting for food
        var waitingForFood = state.Customers.FirstOrDefault(c =>
            c.State == CustomerState.WaitingForFood);
        if (waitingForFood != null)
        {
            return new StaffTask(StaffTaskType.Serve, waitingForFood.Name);
        }

        // Nothing to do - can clean if needed
        return null;
    }
}
