# Unity Global Event Bus
**A decoupled messaging system for Unity toolsets using the Observer Pattern.**

---

### Overview
In game development, "Spaghetti Code" occurs when systems are too tightly coupled (e.g., a Player script manually searching for a UI script via `GameObject.Find` or `GetComponent`). 

This project provides a **Global Event Bus** that acts as a central "Radio Station." It facilitates the transfer of data and signals across separate Unity toolsets, allowing them to communicate without ever needing a direct reference to one another.

### How Data Flows

```mermaid
graph TD
    A[Broadcaster: UserCredits.cs] -->|Publish 'OnCreditSpent'| B(<b>Global Event Bus</b>)
    B -->|Notify| C[Listener: UIManager.cs]
    B -->|Notify| D[Listener: SoundManager.cs]
    B -->|Notify| E[Listener: SaveSystem.cs]
