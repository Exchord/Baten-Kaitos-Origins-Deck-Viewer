# Baten Kaitos Origins Deck Viewer

This tool displays real-time information during battles, including the entire deck and lots of hidden variables.

[Download](https://github.com/Exchord/Baten-Kaitos-Origins-Deck-Viewer/releases)

Information on enemies and magnus: [BKO Documentation](https://docs.google.com/spreadsheets/d/1wXsL9PXnyIuvRiYNgX5p6uTaVBgJhXU1CDzXNFiwLRU/view#gid=1457790647)

![](https://i.imgur.com/036RrOn.png)

### Dolphin
This program is compatible with Dolphin 5.0 and 4.0.x (stable versions only). It automatically hooks to Dolphin and detects emulator and game versions, so no user input is required.

### Context menu
If you right-click any card, value, or cell in this program, a context menu will open, showing the memory address(es) where the right-clicked data comes from.  
You can then click a memory address to copy it to the clipboard, and use it in the memory watch tool of your choice.
The deck viewer itself never writes to Dolphin's process memory.

If you right-click the background, you can select one of three menu options:
- Temporary boost: open temporary boost window
- Show physical addresses: By default, the context menu displays virtual addresses. If this setting is checked, physical addresses are shown instead. If unsure, leave this unchecked.
- Deck Viewer: Displays the version number. Clicking it will open this repository.

### Keyboard shortcuts
- B: open or close temporary boost window
- Escape: close temporary boost window
- R: resize window to fit battle data
- F1: view this documentation

### Battle ID
Each battle ID corresponds to a different set of enemies. You can see this number as soon as a battle transition begins.

### MP
MP for your party, the enemy and - if present - an AI partner.

When performing an MP burst, a timer is shown instead of MP. After initiating a burst, you have 3 seconds to select a card before it deactivates. At the end of an MP burst combo, there is a 1-second time window in which an additional card can be selected. After the burst ends, MP will be stuck at 0 for 35 seconds.  
If applicable, the timer is displayed instead of MP, and the context menu will reveal the memory address of that timer in addition to MP.

### Battle results
Whenever an enemy is defeated, their EXP, TP, gold, and magnus are added to the battle results. TP bonus from combos is also added during battles.
At the end of a battle, EXP is divided by the number of party members, and EXP and gold are multiplied by bonus factors from specific quest magnus.

The game can track a maximum of 8 magnus drops, but only the first 3 magnus are added to the inventory. The remaining magnus are still displayed on the results screen, but they are only added to field guide entries. They are transparent in the deck viewer.

### Deck
When using or discarding a magnus, it temporarily disappears from the deck, but eventually shows up transparent at the end of the deck. All transparent cards are shuffled as soon as one of them enters the slot labeled "NEXT".  

The context menu for each card holds two memory addresses:
- The first address is the magnus ID. It is located in a memory region that is temporarily used during battle, so editing a magnus ID only affects the current battle.
- The second option links to the card's entry in the game's magnus lookup table, which is where all magnus data is stored. Data for one magnus spans 208 bytes in the Japanese version and 188 bytes in the English version. Editing magnus data will affect the entire play session, not just the current battle.

### Characters
The first column displays the name of each character. The context menu links to the memory block containing temporary data for a character, such as their current HP.

If you right-click an AI partner's name, two addresses will appear:
- The first address is the partner's ID. Editing the value will change the AI partner for subsequent battles. This also persists after saving and loading. Change the value to 0 to remove the AI partner.
- The second option links to a character's entry in the enemy lookup table. If the AI partner is not an enemy, it links to a different lookup table specific to AI partners, where stats change on level-ups. Note that making changes to enemy or partner data will affect the entire play session.

### HP
Each character's current HP is displayed in this column. For boss battles that are interrupted when the enemy's HP drops below a certain threshold, the amount of damage required to end the fight (also known as effective HP) is shown in parentheses next to the enemy's HP.

### Delay
Delay is the amount of time left until a party member can select cards again, or until someone is added to the queue in the top left corner of the screen.
There are two memory addresses at play:
- The first address holds the main delay timer. Its total time depends on the attacks used.
- The second address holds a different timer that counts down from 0.5 seconds after a party member has finished preparing a turn, or, for enemies and AI partners, counts down from 2 seconds when the first timer expires.

The first timer is affected by speed-boosting mechanics, such as Pegasus Feather, Speed Aura, or armor that changes turnover speed. The amount of time shown in the table is adjusted based on the character's current speed.  
The second timer is not affected by speed-boosting.

### Down
This is the amount of time a character stays knocked down, unconscious, or asleep. It is affected by speed-boosting. The delay timer is frozen while the character is down.

### Crush
This value increases with each hit a character receives, gradually reducing their total defense. If it reaches a certain threshold, the character will get knocked down or unconscious at the end of the combo. After a combo ends, crush is reset to 0, restoring the initial total defense.

### Status effects
There are two timers in the Poison column:
- The first timer shows the amount of time the character stays poisoned.
- The second timer shows the amount of time left until the next time they take poison damage. It is shown in parentheses next to the other timer.

The Effect column displays timers for 4 more status effects: flames, freezing, shock, and blindness. Without hacking, these effects are mutually exclusive, which is why they share one column.
The context menu has addresses for five different timers, the first two of which are for flames and flame damage (similar to poison and poison damage).

### Auras
In the rightmost column, you can see how much time remains until an aura disappears. Auras last a total of 10 minutes in battles, but they disappear when the party member dies.

### Temporary boost
Right-click the background and click "Temporary boost" for a secondary window where each character's temporary stat boosts are displayed: turnover speed, offenses for each element, defenses for each element, and resistances to each status effect.

For every type of boost, there are two values: One for the next turn, and one for the turn after.  
When a character's turn ends, their speed and offense boosts advance one turn, and the second value is reset to 0.  
Similarly, when a character stops being attacked, their defense and resistance boosts advance one turn, and the second value is reset to 0.  
You can right-click any cell to retrieve its memory address.