# Unity Global Event Bus
**A lightweight decoupled messaging system for Unity toolsets using the Observer Pattern.**

---
<br>

### Overview
In Unity development, "Spaghetti Code" occurs when systems are too tightly coupled (e.g., a UserCredits script manually searching for a UI script via `GameObject.Find` or `GetComponent`). 

This project provides a **Global Event Bus** that acts as a central "Radio Station." It facilitates the transfer of data and signals across separate Unity toolsets, allowing them to communicate without ever needing a direct reference to one another.

### Architecture: Before vs. After

#### Before: Tight Coupling (Spaghetti Code)
In this scenario, the `UserCredits` script is "married" to the `UIManager` and `SoundManager`. If you delete the UI, the UserCredits script crashes.
```csharp
// UserCredits.cs
public class UserCredits : MonoBehaviour {
    public UIManager ui;       // Hard reference
    public SoundManager sfx;    // Hard reference

    void OnCreditSpent() {
        ui.DecreaseBalance();     // If ui is missing, unity breaks
        sfx.PlayBalanceSound();   // If sfx is missing, unity breaks
    }
}
```

#### After: Decoupled (Event Bus)
The `UserCredits` script is now "independent." It simply shouts that credits have been spent. It doesn't care if a UI exists, if a Sound System is listening, or if any other script is tracking it.
```csharp
// UserCredits.cs
public class UserCredits : MonoBehaviour {
    void OnCreditSpent() {
        EventBus.Publish("CreditSpent"); // Just broadcast the signal
    }
}
```

### How Data Flows

```mermaid
graph TD
    A[Broadcaster: UserCredits.cs] -->|Publish 'CreditSpent'| B(<b>Global Event Bus</b>)
    B -->|Notify| C[Listener: UIManager.cs]
    B -->|Notify| D[Listener: SoundManager.cs]
    B -->|Notify| E[Listener: SaveSystem.cs]
