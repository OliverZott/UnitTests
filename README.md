# Resource
- https://trello.com/b/R7tHTQ0H/c-net
- https://www.udemy.com/course/unit-testing-csharp/learn/lecture/8997840#overview

# Remarks

- Last Tutorial is nice wrap-up: https://www.udemy.com/course/unit-testing-csharp/learn/lecture/8997630#overview

- See method as **Blackbox** and don't care about nimplementation (might be wrong!)
- Loosely couple classes --> Programm against Interfaces and use DI. 
  - this also satisfies **Single Responsibility** and **Separation of concerns**
  - SendingEmails has nothing to do in Service class for housekeeper.
- Differentiate testing by **Integration Operation Segregation​ Principle​** for
  - **State Tests** ... return values
  - **Interaction Tests** ... for composition-methods which use different interfaces/classes/services
