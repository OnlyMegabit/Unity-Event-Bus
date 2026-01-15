# Unity Global Event Bus
**A decoupled messaging system for Unity toolsets using the Observer Pattern.**

---

### Overview
In game development, "Spaghetti Code" occurs when systems are too tightly coupled (e.g., a Player script manually searching for a UI script via `GameObject.Find` or `GetComponent`). 

This project provides a **Global Event Bus** that acts as a central "Radio Station." It facilitates the transfer of data and signals across separate Unity toolsets, allowing them to communicate without ever needing a direct reference to one another.

### 🔄 Architecture: Before vs. After

#### ❌ Before: Tight Coupling (Spaghetti Code)
In this scenario, the `Player` script is "married" to the `UIManager` and `SoundManager`. If you delete the UI, the Player script crashes.
```csharp
// Player.cs
public class Player : MonoBehaviour {
    public UIManager ui;       // Hard reference
    public AudioSource sfx;    // Hard reference

    void Die() {
        ui.ShowGameOver();     // If ui is missing, game breaks
        sfx.PlayDieSound();
    }
}
```

#### ✅ After: Decoupled (Event Bus)
The `Player` script is now "independent." It simply shouts that it died. It doesn't care if a UI exists, if a Sound System is listening, or if an AI is tracking player deaths.
```csharp
// Player.cs
public class Player : MonoBehaviour {
    void Die() {
        EventBus.Publish("PlayerDied"); // Just broadcast the signal
    }
}
```

### How Data Flows

```mermaid
graph TD
    A[Broadcaster: UserCredits.cs] -->|Publish 'OnCreditSpent'| B(<b>Global Event Bus</b>)
    B -->|Notify| C[Listener: UIManager.cs]
    B -->|Notify| D[Listener: SoundManager.cs]
    B -->|Notify| E[Listener: SaveSystem.cs]
