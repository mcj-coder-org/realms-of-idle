# Gibberish Speech Synthesis System — Design Document

## 1. Overview

This document describes the design of a procedural "gibberish speech" audio system for a retro pixel art idle RPG. The system produces non-verbal vocalisations that _imply_ speech — short tonal bursts synchronised to displayed text — without using intelligible words. It is modelled on the approach popularised by Undertale, Animal Crossing, and Banjo-Kazooie, adapted for a multi-race fantasy setting.

The architecture combines **Approach 1 (Per-Character Waveform Synthesis)** for its authentic 8-bit simplicity with **Approach 5 (Hybrid Rules Engine with Emotion Tags)** for designer-friendly parameterisation and scalability across a large cast.

---

## 2. Goals & Constraints

| Goal               | Detail                                                                           |
| ------------------ | -------------------------------------------------------------------------------- |
| Retro authenticity | Output should feel like 8-bit / early 16-bit era sound                           |
| Zero voice assets  | All audio is procedurally generated at runtime                                   |
| Tiny CPU budget    | Idle game context — audio must not compete with simulation tick                  |
| Cast scalability   | Support 10+ races × 2+ sexes × 6+ emotions without combinatorial asset explosion |
| Cross-platform     | Identical behaviour on web (Blazor WASM) and mobile (MAUI)                       |
| Designer control   | Narrative writers tag lines with emotion; no audio engineering required          |

---

## 3. High-Level Architecture

```
┌─────────────────────────────────────────────────────┐
│                   Dialogue Data                      │
│  "text": "Welcome to the village!",                  │
│  "speaker": "elder_oak",                             │
│  "emotion": "warm"                                   │
└──────────────────────┬──────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────┐
│              Rules Engine (Approach 5)                │
│                                                      │
│  1. Resolve speaker → VoiceProfile                   │
│  2. Resolve emotion → EmotionModifier                │
│  3. Merge profile + modifier → SynthParams           │
│  4. Analyse text → syllable sequence + prosody curve  │
└──────────────────────┬──────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────┐
│         Waveform Synthesiser (Approach 1)             │
│                                                      │
│  For each syllable:                                  │
│    • Generate tone (waveform + frequency + duration)  │
│    • Apply pitch from prosody curve                   │
│    • Apply volume envelope (ADSR)                     │
│    • Apply DSP effects (filter, distortion, reverb)   │
│    • Schedule playback at correct time offset          │
└─────────────────────────────────────────────────────┘
```

---

## 4. Voice Profile Schema

Every speaking character references a `VoiceProfile`. Profiles are defined per race/sex archetype and can be overridden per individual NPC.

### 4.1 VoiceProfile Definition

```jsonc
{
  "profileId": "elf_female",
  "race": "elf",
  "sex": "female",

  // Base synthesis parameters
  "waveform": "sine", // sine | square | sawtooth | triangle | noise
  "baseFrequency": 320, // Hz — fundamental pitch
  "frequencyVariance": 40, // Hz — random per-syllable jitter
  "syllableDuration": 60, // ms — length of each tone
  "syllableGap": 30, // ms — silence between tones
  "vibratoRate": 6.0, // Hz — vibrato oscillation speed
  "vibratoDepth": 8, // Hz — vibrato pitch swing

  // Volume envelope (ADSR)
  "attack": 0.005, // seconds
  "decay": 0.02, // seconds
  "sustain": 0.7, // 0.0–1.0 multiplier
  "release": 0.03, // seconds

  // DSP chain
  "filterType": "lowpass", // lowpass | highpass | bandpass | none
  "filterFrequency": 2000, // Hz
  "filterQ": 1.0, // resonance
  "distortion": 0.0, // 0.0–1.0
  "reverbMix": 0.05, // 0.0–1.0 (dry/wet)

  // Pitch behaviour
  "pitchRange": [280, 400], // Hz — clamp for prosody
  "questionRise": 1.4, // multiplier on final 3 syllables for '?'
  "exclamationSpike": 1.3, // multiplier on final syllable for '!'
  "commaDropRate": 0.85, // multiplier at commas (micro-pause feel)
}
```

### 4.2 Race Archetype Profiles

| Race          | Waveform | Base Freq (M/F) | Syllable ms | Key Characteristics            |
| ------------- | -------- | --------------- | ----------- | ------------------------------ |
| **Human**     | square   | 160 / 240       | 55          | Balanced, slight filter warmth |
| **Elf**       | sine     | 200 / 320       | 50          | Smooth, vibrato, airy highpass |
| **Dwarf**     | sawtooth | 100 / 150       | 70          | Gritty, low, heavy distortion  |
| **Orc**       | sawtooth | 80 / 130        | 80          | Very low, harsh, slow cadence  |
| **Goblin**    | square   | 300 / 380       | 35          | High, rapid, chaotic jitter    |
| **Fairy**     | sine     | 500 / 600       | 30          | Very high, fast, heavy vibrato |
| **Undead**    | noise    | 120 / 160       | 90          | Noise-based, filtered, eerie   |
| **Construct** | triangle | 180 / 180       | 65          | Monotone, no vibrato, robotic  |
| **Dragon**    | sawtooth | 60 / 90         | 100         | Deepest, distortion + reverb   |
| **Beastfolk** | square   | 140 / 200       | 50          | Mid-range with pitch jumps     |

### 4.3 Sex Modifiers

Sex applies as a delta on top of the race archetype:

```jsonc
{
  "male": {
    "frequencyMultiplier": 1.0, // baseline
    "syllableDurationAdd": 5, // slightly slower
    "vibratoDepthMultiplier": 0.8, // less vibrato
  },
  "female": {
    "frequencyMultiplier": 1.5, // higher pitch
    "syllableDurationAdd": -5, // slightly faster
    "vibratoDepthMultiplier": 1.2, // more vibrato
  },
  "neutral": {
    "frequencyMultiplier": 1.2,
    "syllableDurationAdd": 0,
    "vibratoDepthMultiplier": 1.0,
  },
}
```

### 4.4 Age Modifiers (Optional)

```jsonc
{
  "child": { "frequencyMultiplier": 1.8, "syllableDurationAdd": -10 },
  "young": { "frequencyMultiplier": 1.2, "syllableDurationAdd": -3 },
  "adult": { "frequencyMultiplier": 1.0, "syllableDurationAdd": 0 },
  "elder": {
    "frequencyMultiplier": 0.85,
    "syllableDurationAdd": 15,
    "vibratoDepthMultiplier": 1.5,
  },
}
```

---

## 5. Emotion Tag System

### 5.1 EmotionModifier Schema

```jsonc
{
  "emotionId": "angry",

  // Overrides applied on top of resolved voice profile
  "frequencyShift": 20, // Hz added to base
  "syllableDurationMultiplier": 0.8, // faster
  "syllableGapMultiplier": 0.6, // tighter gaps
  "volumeMultiplier": 1.3, // louder
  "distortionAdd": 0.15, // more grit
  "vibratoRateMultiplier": 1.5, // agitated vibrato
  "pitchVarianceMultiplier": 1.4, // more erratic
}
```

### 5.2 Emotion Catalogue

| Emotion        | Freq Shift | Duration | Gap   | Volume | Distortion | Vibrato | Variance | Feel                     |
| -------------- | ---------- | -------- | ----- | ------ | ---------- | ------- | -------- | ------------------------ |
| **neutral**    | 0          | ×1.0     | ×1.0  | ×1.0   | +0.0       | ×1.0    | ×1.0     | Baseline                 |
| **happy**      | +15        | ×0.9     | ×0.8  | ×1.1   | +0.0       | ×1.2    | ×1.2     | Bright, bouncy           |
| **sad**        | −20        | ×1.3     | ×1.4  | ×0.8   | +0.0       | ×0.6    | ×0.5     | Slow, flat               |
| **angry**      | +20        | ×0.8     | ×0.6  | ×1.3   | +0.15      | ×1.5    | ×1.4     | Harsh, fast              |
| **scared**     | +15        | ×1.4     | ×1.6  | ×0.6   | +0.0       | ×2.0    | ×1.5     | Slow, trembling whisper  |
| **excited**    | +25        | ×0.85    | ×0.8  | ×1.2   | +0.0       | ×1.3    | ×1.5     | Fast, high energy        |
| **warm**       | +5         | ×1.1     | ×1.1  | ×1.0   | +0.0       | ×1.1    | ×0.8     | Gentle, even             |
| **cold**       | −10        | ×1.2     | ×1.3  | ×0.9   | +0.0       | ×0.3    | ×0.3     | Flat, clipped            |
| **mysterious** | −5         | ×1.15    | ×1.2  | ×0.85  | +0.0       | ×0.8    | ×0.6     | Slow, reverb-heavy       |
| **panicked**   | +30        | ×0.75    | ×0.55 | ×1.3   | +0.12      | ×2.0    | ×2.0     | Fast, frantic, distorted |

---

## 6. Dialogue Markup Format

Narrative writers author dialogue lines using inline emotion tags. The markup is lightweight and embeds within standard JSON dialogue files.

### 6.1 Line Format

```jsonc
{
  "id": "quest_01_greeting",
  "speaker": "elder_oak", // → resolves to VoiceProfile
  "lines": [
    {
      "text": "Ah, you've finally arrived.",
      "emotion": "warm",
    },
    {
      "text": "The forest has been... unsettled.",
      "emotion": "mysterious",
      "pause_before": 400, // ms pause before this line
    },
    {
      "text": "Will you help us?",
      "emotion": "warm",
    },
  ],
}
```

### 6.2 Speaker Registry

```jsonc
{
  "speakerId": "elder_oak",
  "displayName": "Elder Oak",
  "voiceProfile": "elf_male", // archetype reference
  "age": "elder", // age modifier
  "overrides": {
    // optional per-NPC tweaks
    "baseFrequency": 175, // slightly lower than elf_male default
    "reverbMix": 0.15, // more reverb for gravitas
  },
}
```

### 6.3 Inline Emotion Switching

For mid-sentence emotion changes:

```jsonc
{
  "text": "I thought we were safe, but {scared}THEY found us{/scared}... {sad}it's over.",
  "emotion": "neutral", // default emotion for untagged segments
}
```

The parser splits this into three segments, each synthesised with its own emotion modifier while maintaining continuous syllable timing.

### 6.4 Full Worked Example

**Scene:** A dwarf blacksmith greets the player, then reacts to news of a dragon.

```jsonc
{
  "id": "smithy_dragon_news",
  "speaker": "brondar",
  "lines": [
    { "text": "Ha! Come in, come in!", "emotion": "happy" },
    { "text": "What's that? A dragon?", "emotion": "excited" },
    { "text": "In the eastern mines?!", "emotion": "scared" },
    { "text": "We need to warn the king.", "emotion": "cold" },
  ],
}
```

**Speaker registry:**

```jsonc
{
  "speakerId": "brondar",
  "displayName": "Brondar the Smith",
  "voiceProfile": "dwarf_male",
  "age": "adult",
  "overrides": {
    "distortion": 0.25, // extra gravel in his voice
  },
}
```

**Synthesis resolution for line 3 ("In the eastern mines?!"):**

| Parameter     | Dwarf Base | Male Mod   | Scared Mod  | Final        |
| ------------- | ---------- | ---------- | ----------- | ------------ |
| Frequency     | 100 Hz     | ×1.0 = 100 | +30 = 130   | **130 Hz**   |
| Syllable ms   | 70         | +5 = 75    | ×0.7 = 52   | **52 ms**    |
| Gap ms        | 35         | —          | ×0.5 = 17   | **17 ms**    |
| Waveform      | sawtooth   | —          | —           | **sawtooth** |
| Distortion    | 0.25\*     | —          | +0.0        | **0.25**     |
| Vibrato depth | 8 Hz       | ×0.8 = 6.4 | ×2.0 = 12.8 | **12.8 Hz**  |
| Volume        | 1.0        | —          | ×0.9        | **0.9**      |

_\*NPC override applied_

The `?!` ending applies both `questionRise` (×1.4 on last 3 syllables) and `exclamationSpike` (×1.3 on final syllable), stacking multiplicatively. The final syllable of "mines" hits 130 × 1.4 × 1.3 = **236.6 Hz**.

---

## 7. Syllable Parser

The text-to-syllable engine is intentionally simple — it does not need linguistic accuracy, only plausible rhythm.

### 7.1 Algorithm

```
1. Strip markup tags, retain segment boundaries
2. Split text on whitespace → words
3. For each word:
   a. Count vowel clusters (a, e, i, o, u, y) → syllable count
   b. Minimum 1 syllable per word
   c. Map each syllable to a pitch offset:
      - Vowel-heavy syllables → slightly higher pitch
      - Consonant-heavy → slightly lower, shorter
4. Detect punctuation:
   - '?' → apply questionRise to last 3 syllables
   - '!' → apply exclamationSpike to last syllable
   - ',' → insert micro-pause (commaDropRate)
   - '...' → insert long pause (3× syllableGap)
   - '.' → apply gentle pitch drop on last 2 syllables
5. Generate prosody curve (array of frequency multipliers)
```

### 7.2 Prosody Curve Example

Text: `"Will you help us?"`

| Syllable | Source | Base Mult | Punctuation Mod   | Final Mult |
| -------- | ------ | --------- | ----------------- | ---------- |
| Will     | "Will" | 1.00      | —                 | 1.00       |
| you      | "you"  | 1.02      | —                 | 1.02       |
| help     | "help" | 0.98      | questionRise ×1.4 | 1.37       |
| us       | "us"   | 1.01      | questionRise ×1.4 | 1.41       |

The rising pitch on the last two syllables creates the unmistakable "question" inflection.

---

## 8. Platform Implementation

### 8.1 Shared Core (C#)

The following components are pure C# with zero platform dependencies:

- **SyllableParser** — text → syllable sequence + prosody curve
- **VoiceProfileResolver** — speaker ID + emotion → merged `SynthParams`
- **RulesEngine** — applies emotion modifiers, age modifiers, NPC overrides
- **DialogueMarkupParser** — parses inline emotion tags

These live in a shared .NET Standard / .NET 8 class library referenced by both MAUI and Blazor projects.

### 8.2 Web (Blazor WASM)

```
Blazor Component
  │
  ├─ C# SyllableParser + RulesEngine (shared library)
  │
  └─ JS Interop → Web Audio API
       ├─ OscillatorNode (waveform generation)
       ├─ GainNode (ADSR envelope)
       ├─ BiquadFilterNode (lowpass/highpass)
       ├─ WaveShaperNode (distortion)
       └─ ConvolverNode (reverb)
```

**Key implementation notes:**

- Use `AudioContext.resume()` on first user interaction to satisfy browser autoplay policies
- Pre-create an `AudioContext` at page load, reuse across all speech events
- Schedule tones using `oscillator.start(time)` with precise offsets from `audioContext.currentTime`
- Keep the JS interop layer thin — pass a serialised `SynthCommand[]` array from C# and let JS iterate it

**JS Interop surface (minimal):**

```typescript
interface SynthCommand {
  startTime: number; // seconds offset from now
  frequency: number; // Hz
  duration: number; // seconds
  waveform: string; // "sine" | "square" | "sawtooth" | "triangle"
  volume: number; // 0.0–1.0
  vibratoRate: number;
  vibratoDepth: number;
  filterType: string;
  filterFreq: number;
  filterQ: number;
  distortion: number;
}

function playSpeech(commands: SynthCommand[]): void;
function stopSpeech(): void;
```

### 8.3 Mobile (MAUI)

Two viable paths, choose based on project complexity:

#### Option A: MAUI Blazor Hybrid (Recommended)

If the game UI is already Blazor Hybrid, use the **identical Web Audio API path** as web. The WebView supports Web Audio on both Android and iOS. This gives you a single audio implementation across all platforms.

**Caveats:**

- Android WebView requires user gesture to unlock `AudioContext` — trigger on first tap
- iOS WKWebView supports Web Audio but may have ~50ms extra latency — acceptable for speech burbles
- Test on low-end devices; Web Audio in WebView can be heavier than native

#### Option B: Native Audio via FMOD (Advanced)

For pure MAUI or if WebView audio proves unreliable:

```
MAUI App
  │
  ├─ C# SyllableParser + RulesEngine (shared library)
  │
  └─ C# FMOD Wrapper (NuGet: FmodSharp or manual binding)
       ├─ FMOD.System → DSP chain
       ├─ FMOD.DSP.Oscillator (waveform)
       ├─ FMOD.DSP.LowPass / HighPass
       ├─ FMOD.DSP.Distortion
       └─ FMOD.Channel (scheduling + volume)
```

**FMOD advantages:**

- Native performance, sub-10ms latency
- Identical API on Android, iOS, Windows, macOS
- Free for games under $200k revenue
- Rich DSP chain with real-time parameter control

**FMOD disadvantages:**

- Native library bundling per platform (~2MB per arch)
- More complex build setup
- Licensing required above revenue threshold

#### Option C: Raw PCM Fallback

For maximum simplicity with no dependencies:

```csharp
// Generate PCM samples in C#
float[] GenerateTone(float freq, float duration, int sampleRate = 44100) {
    var samples = new float[(int)(duration * sampleRate)];
    for (int i = 0; i < samples.Length; i++) {
        float t = i / (float)sampleRate;
        samples[i] = MathF.Sin(2 * MathF.PI * freq * t);
    }
    return samples;
}

// Play via Plugin.Maui.Audio or platform AudioTrack/AVAudioEngine
```

This works but lacks real-time DSP (filter, distortion). Suitable for a simpler voice palette.

### 8.4 Platform Decision Matrix

| Criterion             | Blazor Hybrid (Web Audio) | FMOD Native         | Raw PCM             |
| --------------------- | ------------------------- | ------------------- | ------------------- |
| Code sharing with web | 100%                      | Shared C# core only | Shared C# core only |
| Audio latency         | ~50–100ms                 | ~5–15ms             | ~20–50ms            |
| DSP capabilities      | Full (Web Audio nodes)    | Full (FMOD DSP)     | Manual only         |
| Bundle size impact    | 0                         | ~2MB per arch       | 0                   |
| Setup complexity      | Low                       | Medium              | Low                 |
| Reliability           | Good (WebView quirks)     | Excellent           | Good                |

**Recommendation:** Start with Blazor Hybrid + Web Audio for both web and mobile. This gives a single implementation. If mobile audio quality or latency is insufficient, migrate the mobile path to FMOD — the shared C# core (parser, rules engine, profile resolver) remains unchanged. Only the `playSpeech()` backend swaps out.

---

## 9. Text Display Synchronisation

The speech system must synchronise with the typewriter-style text display common in RPGs.

### 9.1 Timing Model

```
Text: "Hello there!"
Display: H·e·l·l·o· ·t·h·e·r·e·!
Speech:  [tone]  [tone] [tone]  [tone]
         "Hel"   "lo"   "there" (!)
```

Characters display one at a time. Tones trigger at the _start_ of each syllable's first character. The display rate and speech rate are coupled:

```
displayInterval = syllableDuration + syllableGap
charsPerSyllable = word.length / syllableCount
charInterval = displayInterval / charsPerSyllable
```

This ensures the last tone finishes as the last character appears.

### 9.2 Pause Handling

| Punctuation | Display Pause | Speech Behaviour                 |
| ----------- | ------------- | -------------------------------- |
| `,`         | +100ms        | Pitch drops × commaDropRate      |
| `.`         | +200ms        | Gentle pitch fall, silence       |
| `...`       | +500ms        | Silence, dramatic pause          |
| `?`         | +200ms        | Rising pitch on last 3 syllables |
| `!`         | +150ms        | Spike on final syllable          |
| `?!`        | +200ms        | Rise + spike (multiplicative)    |

---

## 10. Scene Chatter System

### 10.1 Overview

Beyond foreground dialogue, the speech system drives ambient NPC conversations that bring scenes to life. NPCs in the game world conduct audible back-and-forth exchanges with speech bubbles appearing above their heads, synchronised to the gibberish audio. Volume and stereo position are derived from the NPC's spatial relationship to the player character.

### 10.2 Conversation Model

Chatter is not random babble — it is structured as **turn-taking conversations** between 2–3 NPCs who are spatially near each other.

```
┌──────────────────────────────────────────────────┐
│              ConversationGroup                    │
│                                                   │
│  participants: [NPC_A, NPC_B]                     │
│  script: ConversationScript                       │
│  state: active | paused | complete                │
│  currentTurn: 0                                   │
│  position: { x, y } (midpoint of participants)    │
└──────────────────────────────────────────────────┘
```

#### Turn Sequencing

```jsonc
{
  "scriptId": "tavern_gossip_01",
  "tags": ["tavern", "casual", "gossip"],
  "turns": [
    { "speaker": 0, "text": "Did you hear about the mines?", "emotion": "mysterious" },
    { "speaker": 1, "text": "No, what happened?", "emotion": "neutral" },
    { "speaker": 0, "text": "They found something... ancient.", "emotion": "scared" },
    { "speaker": 1, "text": "That cannot be good.", "emotion": "cold" },
    { "speaker": 0, "text": "We should tell the elder.", "emotion": "warm" },
  ],
  "loop": false,
  "pauseBetweenTurns": { "min": 400, "max": 1000 },
}
```

Each turn plays sequentially: speaker 0 speaks, a randomised pause occurs, then speaker 1 responds. The conversation advances through the script and either completes or loops.

#### Conversation Assignment

The scene manager assigns conversations at runtime:

```
1. Query all NPCs in the current scene
2. Group NPCs by proximity (within N tiles of each other)
3. For each group, select a ConversationScript matching scene tags
4. Assign speaker indices to NPCs based on voice compatibility
5. Start conversations with staggered offsets (so not all begin simultaneously)
```

Multiple conversations can run concurrently in a scene (e.g. two pairs of NPCs chatting in different corners of a tavern).

### 10.3 Speech Bubble Display

When an NPC speaks a chatter line, a speech bubble appears above their sprite:

```
        ┌─────────────────────────┐
        │ Did you hear about the  │
        │ mines?                  │
        └────────────┬────────────┘
                     ▼
                 [NPC Sprite]
```

#### Bubble Behaviour

| Property    | Behaviour                                                      |
| ----------- | -------------------------------------------------------------- |
| Appear      | Fade in over 100ms when turn starts                            |
| Text reveal | Typewriter sync from speech timeline (identical to foreground) |
| Persist     | Remains 800ms after audio completes so player can read         |
| Disappear   | Fade out over 200ms                                            |
| Overlap     | If NPC speaks again before fade completes, reuse bubble        |
| Off-screen  | Bubbles are culled when NPC is off-screen                      |
| Max width   | 160px (scaled to game resolution), line-wrap if needed         |
| Font        | Game's pixel font at reduced size vs foreground dialogue       |

#### Distance-Based Bubble Visibility

Bubbles have three display tiers based on distance from the player character:

| Distance (tiles) | Bubble Display                      | Text               |
| ---------------- | ----------------------------------- | ------------------ |
| 0–6 (near)       | Full bubble with typewriter text    | Fully readable     |
| 7–12 (mid)       | Small bubble with "..." placeholder | Implies speech     |
| 13+ (far)        | No bubble                           | Audio only (faint) |

### 10.4 Spatial Audio

#### Stereo Panning

NPC position on screen maps to stereo pan position:

```
Screen left edge          Centre          Screen right edge
     -1.0 ──────────────── 0.0 ──────────────── +1.0
```

```
panValue = clamp((npc.screenX - viewport.centreX) / (viewport.width / 2), -1.0, 1.0)
```

The pan value is passed to a `StereoPannerNode` (Web Audio) or FMOD channel pan parameter.

#### Distance-Based Volume Falloff

Volume attenuates with distance from the player character using an inverse curve with a minimum floor (so distant chatter is faint but not silent):

```
distanceTiles = euclidean(player.pos, npc.pos)
falloff = max(0.05, 1.0 - (distanceTiles / maxAudibleDistance))
finalVolume = baseChatterVolume × emotionVolume × falloff
```

| Distance (tiles) | Falloff (maxAudible=20) | Effective volume at 30% base |
| ---------------- | ----------------------- | ---------------------------- |
| 0                | 1.00                    | 30%                          |
| 5                | 0.75                    | 22%                          |
| 10               | 0.50                    | 15%                          |
| 15               | 0.25                    | 7.5%                         |
| 20+              | 0.05                    | 1.5% (floor)                 |

#### Combined Spatial Parameters

Each chatter voice resolves spatial audio per frame:

```csharp
public struct SpatialParams
{
    public float Pan;        // -1.0 to +1.0
    public float Volume;     // 0.0 to 1.0 (post-falloff)
    public bool  InRange;    // false if beyond maxAudibleDistance
    public int   BubbleTier; // 0=near, 1=mid, 2=far
}

public SpatialParams ResolveSpatial(Vector2 npcPos, Vector2 playerPos, Viewport vp)
{
    float dist = Vector2.Distance(npcPos, playerPos);
    float screenX = WorldToScreen(npcPos).X;

    return new SpatialParams
    {
        Pan = Math.Clamp((screenX - vp.Width / 2f) / (vp.Width / 2f), -1f, 1f),
        Volume = Math.Max(0.05f, 1f - dist / MaxAudibleDistance),
        InRange = dist <= MaxAudibleDistance,
        BubbleTier = dist <= 6 ? 0 : dist <= 12 ? 1 : 2,
    };
}
```

### 10.5 Foreground Ducking

When the player initiates foreground dialogue (talking to an NPC directly), all background chatter ducks:

| Event               | Chatter Volume        | Transition      |
| ------------------- | --------------------- | --------------- |
| Dialogue box opens  | ×0.3 (30% of current) | Fade over 300ms |
| Dialogue box closes | ×1.0 (restore)        | Fade over 500ms |

Conversations pause their turn timer during ducking so they do not advance while inaudible. They resume from where they left off when ducking lifts.

### 10.6 Scene Transition

When the player moves between scenes (e.g. exiting the tavern into the market):

```
1. Fade all active conversations to silence over 500ms
2. Pause and discard current conversation assignments
3. Load new scene's NPC pool and conversation scripts
4. Assign new conversations with staggered starts
5. Fade in over 800ms
```

The crossfade overlap of ~300ms prevents an abrupt silence gap.

### 10.7 Scene Pool Definition

Each scene defines available NPCs and conversation scripts:

```jsonc
{
  "sceneId": "tavern_main",
  "maxConcurrentConversations": 3,
  "maxAudibleDistance": 20,
  "ambientVolume": 0.25,
  "npcs": [
    {
      "npcId": "barkeep",
      "position": { "x": 12, "y": 8 },
      "voiceProfile": "human_male",
      "age": "adult",
      "overrides": { "baseFrequency": 145 },
    },
    {
      "npcId": "drunk_dwarf",
      "position": { "x": 10, "y": 9 },
      "voiceProfile": "dwarf_male",
      "age": "adult",
      "overrides": { "distortion": 0.3 },
    },
    {
      "npcId": "bard_elf",
      "position": { "x": 15, "y": 6 },
      "voiceProfile": "elf_female",
      "age": "young",
      "overrides": {},
    },
  ],
  "conversationScripts": [
    "tavern_gossip_01",
    "tavern_gossip_02",
    "tavern_complaint_01",
    "tavern_joke_01",
  ],
}
```

---

## 11. Performance Budget

| Metric                    | Target                     | Notes                                                       |
| ------------------------- | -------------------------- | ----------------------------------------------------------- |
| CPU per line              | <2ms setup                 | Schedule all tones upfront, let audio hardware play         |
| Memory                    | <50KB                      | No samples; all generated. Profile data is ~200 bytes each  |
| Foreground voices         | 1                          | RPG dialogue is single-speaker; cut previous on new line    |
| Concurrent chatter voices | 3 max                      | Each is a scheduled tone sequence; Web Audio handles mixing |
| Spatial updates           | Per frame                  | Pan + volume recalc is trivial (<0.1ms for 6 NPCs)          |
| StereoPannerNodes         | 1 per active chatter voice | Created per utterance, GC'd after stop                      |
| Audio latency             | <100ms                     | Acceptable for text-synced speech                           |
| Speech bubble DOM updates | Per rAF tick               | Same timeline-driven approach as foreground typewriter      |

---

## 12. Implementation Roadmap

### Phase 1: Core (Sprint 1–2)

- Syllable parser with punctuation detection
- 3 voice profiles (human male/female, elf female)
- Web Audio JS interop from Blazor
- Basic typewriter sync

### Phase 2: Full Cast (Sprint 3)

- All 10 race archetypes × male/female
- Emotion modifier system (6 emotions)
- Dialogue markup parser with inline tags
- Speaker registry + NPC overrides

### Phase 3: Polish (Sprint 4)

- Remaining 4 emotions
- Age modifiers
- Reverb and advanced DSP effects
- Mobile testing and FMOD fallback if needed

### Phase 4: Scene Chatter (Sprint 5–6)

- Conversation script format and parser
- Turn-taking sequencer with staggered timing
- Speech bubble renderer with typewriter sync
- Distance-based bubble tier system (near/mid/far)

### Phase 5: Spatial Audio (Sprint 7)

- StereoPannerNode integration for positional panning
- Distance-based volume falloff with floor
- Foreground ducking on dialogue open/close
- Scene transition crossfade
- Per-frame spatial parameter updates

### Phase 6: Tools & Content (Sprint 8)

- Voice profile preview tool for designers
- Conversation script editor / preview
- Scene chatter mixer (test multiple concurrent conversations)
- Automated test suite (golden audio fingerprints)
- Performance profiling on target mobile devices
