# Unity Global Event Bus
**A lightweight decoupled messaging system for Unity toolsets using the Observer Pattern.**

---

### Overview
In Unity development, "Spaghetti Code" occurs when systems are too tightly coupled (e.g., a UserCredits script manually searching for a UI script via `GameObject.Find` or `GetComponent`). 

This project provides a **Global Event Bus** that acts as a central "Radio Station." It facilitates the transfer of data and signals across separate Unity toolsets, allowing them to communicate without ever needing a direct reference to one another.

### Architecture: Before vs. After

#### Before: Tight Coupling (Spaghetti Code)
In this scenario, the `UserCredits` script is "married" to the `UIManager`. To update the UI, the script needs a hard reference and specific knowledge of how the UI works. If you delete the UI, the logic breaks.
```csharp
// UserCredits.cs
public class UserCredits : MonoBehaviour {
    public UIManager ui; // Hard reference

    void SpendCredits(int amount) {
        // Logic is stuck: it HAS to know about the UI to work
        ui.UpdateDisplay(amount); 
    }
}
```

#### After: Decoupled (Event Bus)
The `UserCredits` script is now "independent." It simply broadcasts a CreditEvent package. It doesn't care if a UI exists, if a Save System is listening, or if an Analytics script is tracking it.
```csharp
// UserCredits.cs
public struct CreditEvent { 
    public int amount;
}

public class UserCredits : MonoBehaviour {
    void SpendCredits(int amount) {
        // Broadcast the data package to any system that cares
        EventBus<CreditEvent>.Publish("CreditSpent", new CreditEvent { amount = amount });
    }
}
```

### How Data Flows

```mermaid
graph TD
    A[Broadcaster: UserCredits.cs] -->|Publish 'CreditEvent'| B(<b>Global Event Bus &lt;T&gt;</b>)
    B -->|Notify with Data| C[Listener: UIManager.cs]
    B -->|Notify with Data| D[Listener: SaveSystem.cs]
    B -->|Notify with Data| E[Listener: Analytics.cs]
