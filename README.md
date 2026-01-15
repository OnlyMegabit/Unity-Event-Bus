graph LR
    A[Player Script] -- Publish Event --> B((Event Bus))
    B -- Notify --> C[UI Manager]
    B -- Notify --> D[Sound Manager]
    B -- Notify --> E[Analytics Tool]