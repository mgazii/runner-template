# Runner Template

![](https://user-images.githubusercontent.com/22501079/123521671-c3161000-d6c0-11eb-9c23-b19ca9765d1d.PNG)


Runner Template is a Unity template project for hyper-casual runner games, always room for improve

I am currently using Unity 2021.1.12f1. Older versions probably wont be a problem for this project


## Installation

Use [git](https://git-scm.com/downloads) to install Runner Template.

First fork the repository to your account then use
```bash
git clone https://github.com/yourusername/runner-template.git .
```
After cloning you can add the project to Unity

## Usage

#### GameManager
Used For Managing GameStates. Use  for state registration then define a void function with enum GameState's name.

```c#
# GameStateEvents add or remove new states
public enum GameState {PREP,MID,END} 

# registers class to GameStateEvents 
GameManager.Instance.AddGameStateListener(this) 

# runs on PREP state, for another define function with enum GameState's name
void PREP(){} 
```

#### InputManager
 
Detection of Swipe, Slide, Tap, Double Tap or Tapping on objects with collider.

```c#
# tap registration
InputManager.Instance.RegisterTap(UnityAction<Tap> tapFunc) 

# tap function 
void tapFunc(Tap tap){} 
```

Same with swipe and slide.
 
RegisterSlide => UnityAction\<Slide\>

RegisterSwipe => UnityAction\<Swipe\>


## Contributing
It's very simple project. No contribution required

## License
[MIT](https://choosealicense.com/licenses/mit/)
