# [![](https://i.gyazo.com/94476b2151a7b7c5af914957955edb4a.png)](#) Sidekick 2 Beta [![](https://img.shields.io/github/v/release/domialex/sidekick?style=flat-square&include_prereleases)](https://github.com/domialex/Sidekick/releases) [![](https://img.shields.io/discord/664252463188279300?color=%23738AD6&label=Discord&style=flat-square)](https://discord.gg/H4bg4GQ)
A Path of Exile tool to:
- Trade Efficiently;
- View Cheatsheets;
- Check Dangerous Modifiers;
- And More!

## Installation and Usage
1. [Download Sidekick](https://github.com/domialex/Sidekick/releases)
2. Extract the zip file
3. Run Sidekick.exe
4. Optionnally, you may get a security warning when running this software. You may have to add a security exception to run this software. Our code is open source and there is no malware included with this program.
5. You will get a Setup window to select your language preference and your league.
6. You cannot run Path of Exile in Fullscreen mode to use this tool. We recommend using "Windows Fullscreen".
7. Enjoy! Report issues or suggestions in our Discord or create an issue here.

## Features
### Trade
#### Default Binding: Ctrl + D
Opens a trade view from the official Path of Exile trade API. You can compare and preview items by clicking on any result. For rare items, a price prediction from poeprices.info is shown. For unique items, prices from poe.ninja are used.
| Trade |
|---|
| ![](https://raw.githubusercontent.com/domialex/Sidekick/master/docs/assets/images/price-check.png) |

### Cheatsheets
#### Default Binding: F6
Opens a view with useful common information about different mechanics of the game.

| Heist | Betrayal | Incursion | Blight |
|---|---|---|---|
| ![](./docs/assets/images/cheatsheets_heist.png) | ![](./docs/assets/images/cheatsheets_betrayal.png) | ![](./docs/assets/images/cheatsheets_incursion.png) | ![](./docs/assets/images/cheatsheets_blight.png) |

### Map Information
#### Default Binding: Ctrl + X
Checks the modifiers on a map or contract for mods that are dangerous (configurable).

| Safe Map | Unsafe Map |
|---|---|
| ![](./docs/assets/images/map_safe.png) | ![](./docs/assets/images/map_unsafe.png) |

### Chat Commands
| Name | Default Binding | Description |
|---|---|---|
| Go to Hideout | F5 | Quickly go to your hideout. Writes the following chat command: `/hideout` |
| Leave Party | F4 | Quickly leave a party. You must have set your character name in the settings first. Writes the following chat command: `/kick {settings.Character_Name}` |
| Reply to Latest Whisper | Ctrl + Shift + R | Reply to the last whisper received. Starts writing the following chat command: `@{characterName}` |
| Exit to Character Selection | F12 | Exit to the character selection screen. Writes the following chat command: `/exit` |

### Other Features
| Name | Default Binding | Description |
|---|---|---|
| Open Wiki | Alt + W | Open the wiki for the item currently under your mouse. |
| Find Items | Ctrl + F | Search an item in your stash or passive tree. |
| Open Search | Alt + Q | Open the official trade website using the item under your mouse. |

## Development [![](https://img.shields.io/discord/664252463188279300?color=%23738AD6&label=Discord&style=flat-square)](https://discord.gg/H4bg4GQ)
We accept most PR and ideas. If you want a feature included, create an issue and we will discuss it.

We are also available on [Discord](https://discord.gg/H4bg4GQ).

## Thanks
- [Contributors](https://github.com/domialex/Sidekick/graphs/contributors)
- [POE-TradeMacro](https://github.com/PoE-TradeMacro/POE-TradeMacro) - Original idea
- [WindowsHook](https://github.com/topstarai/WindowsHook) - Keyboard and mouse hooks
- [AdonisUI](https://benruehl.github.io/adonis-ui/) - UI
- [poe.ninja](https://poe.ninja/)
- [Poe Price Info](https://www.poeprices.info/)
