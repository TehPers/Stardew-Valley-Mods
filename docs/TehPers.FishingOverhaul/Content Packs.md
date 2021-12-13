# Teh's Fishing Overhaul - Content packs

Each content pack can have several different files. They are all optional.

| File                              | Purpose                                                                                                    |
| --------------------------------- | ---------------------------------------------------------------------------------------------------------- |
| [`content.json`](#content)        | Adds new fishing content.                                                                                  |
| [`fishTraits.json`](#fish-traits) | (Deprecated) Add new fish traits. This configures how the fish behave, but not where/when they are caught. |
| [`fish.json`](#fish)              | (Deprecated) Add new fish availabilities. This configures where/when fish can be caught.                   |
| [`trash.json`](#trash)            | (Deprecated) Add new trash availabilities.                                                                 |
| [`treasure.json`](#treasure)      | (Deprecated) Add new treasure avaailabilities.                                                             |

There are also JSON schemas available for each of these files. If your editor supports JSON schemas, then it is recommended you reference the appropriate schema:

| Editor             | How to reference the schema                                                                         |
| ------------------ | --------------------------------------------------------------------------------------------------- |
| Visual Studio Code | Add a `$schema` property to the root object in the file.                                            |
| Visual Studio      | At the top of the file editor right under the tabs, paste the schema URL into the "Schema" textbox. |

## Content

[**JSON Schema**][content schema]

The `content.json` file is the root content file for your content pack. It controls what fishing content should be added.

| Property        | Type       | Required | Default | Description                           |
| --------------- | ---------- | -------- | ------- | ------------------------------------- |
| `$schema`       | `string`   | No       | N/A     | Optional schema URL.                  |
| `Include`       | `string[]` | No       | N/A     | Additional content files to include.  |
| `AddFishTraits` | `object`   | No       | N/A     | [Fish traits](#fish-traits) to add.   |
| `AddFish`       | `object`   | No       | N/A     | [Fish entries](#fish) to add.         |
| `AddTrash`      | `object`   | No       | N/A     | [Trash entries](#trash) to add.       |
| `AddTreasure`   | `object`   | No       | N/A     | [Treasure entries](#treasure) to add. |

### Fish traits

`AddFishTraits` maps [fish item keys][namespaced key] to the traits for those fish. The possible traits for a fish are:

| Property        | Type      | Required | Default | Description                                       |
| --------------- | --------- | -------- | ------- | ------------------------------------------------- |
| `DartFrequency` | `integer` | Yes      | N/A     | How often the fish darts in the fishing minigame. |
| `DartBehavior`  | `string`  | Yes      | N/A     | How the fish darts during the fishing minigame.   |
| `MinSize`       | `integer` | Yes      | N/A     | The minimum size the fish can be.                 |
| `MaxSize`       | `integer` | Yes      | N/A     | The maximum size the fish can be.                 |
| `IsLegendary`   | `boolean` | No       | `false` | Whether the fish is legendary.                    |

### Fish

Fish entries each configure when a specific fish can be made available. Multiple entries may refer to the same fish, allowing complex customization over when a fish is available and what the chances of catching that fish are.

| Property           | Type     | Required | Default | Description                               |
| ------------------ | -------- | -------- | ------- | ----------------------------------------- |
| `FishKey`          | `string` | Yes      | N/A     | The [namespaced key] for the fish.        |
| `AvailabilityInfo` | `object` | Yes      | N/A     | The fish availability data for the entry. |
| `OnCatch`          | `object` | No       | `{}`    | The [actions] to take on catch.           |

Fish availability determines when a fish is available and includes all the normal availability properties as well.

| Property          | Type      | Required | Default | Description                                                                                                                          |
| ----------------- | --------- | -------- | ------- | ------------------------------------------------------------------------------------------------------------------------------------ |
| `DepthMultiplier` | `number`  | No       | 0.1     | Effect that sending the bobber by less than the max distance has on the chance. This value should be no more than 1. Default is 0.1. |
| `MaxDepth`        | `integer` | No       | 4       | The required fishing depth to maximize the chances of catching the fish. Default is 4.                                               |
| Common fields...  | ...       | ...      | ...     | Other common [availability info] properties.                                                                                         |

### Trash

Trash entries each configure when a specific trash can be made available. Multiple entries may refer to the same trash item, allowing complex customization over when a trash is available and what the chances of catching that trash are.

| Property           | Type     | Required | Default | Description                              |
| ------------------ | -------- | -------- | ------- | ---------------------------------------- |
| `TrashKey`         | `string` | Yes      | N/A     | The [namespaced key] for the trash item. |
| `AvailabilityInfo` | `object` | Yes      | N/A     | The [availability info] for the entry.   |
| `OnCatch`          | `object` | No       | `{}`    | The [actions] to take on catch.          |

The availability uses the common availability properties.

### Treasure

Treasure entries each configure when a specific treasure can be made available. Multiple entries may refer to the same treasure item, allowing complex customization over when a treasure is available and what the chances of catching that treasure are.

| Property           | Type      | Required | Default | Description                                                                                   |
| ------------------ | --------- | -------- | ------- | --------------------------------------------------------------------------------------------- |
| `AvailabilityInfo` | `object`  | Yes      | N/A     | The [availability info] for the entry.                                                        |
| `ItemKeys`         | `array`   | Yes      | N/A     | The possible [namespaced keys][namespaced key] for the loot. The item key is chosen randomly. |
| `MinQuantity`      | `integer` | No       | 1       | The minimum quantity of the item. This is only valid for stackable items.                     |
| `MaxQuantity`      | `integer` | No       | 1       | The maximum quantity of the item. This is only valid for stackable items.                     |
| `AllowDuplicates`  | `boolean` | No       | `true`  | Whether this can be found multiple times in one chest.                                        |
| `OnCatch`          | `object`  | No       | `{}`    | The [actions] to take on catch.                                                               |

The availability uses the common availability properties.

## Common

There are a few common properties and types that are used throughout multiple config files.

### Namespaced key

A namespaced key is a unique identifier that is associated with an item. For example, a namespaced key may refer to wood, pineapples, iridium bands, or any other item in the game. Each namespaced key consists of both a namespace and a key. The format of a namespaced key in a JSON file is `namespace:key`. For vanilla items and items added through vanilla content files, namespaced keys all have the namespace "StardewValley" and follow this format:

| Item type | Key format                                                                  |
| --------- | --------------------------------------------------------------------------- |
| Craftable | `StardewValley:BigCraftable/<id>`                                           |
| Boots     | `StardewValley:Boots/<id>`                                                  |
| Clothing  | `StardewValley:Clothing/<id>`                                               |
| Flooring  | `StardewValley:Flooring/<id>`                                               |
| Furniture | `StardewValley:Furniture/<id>`                                              |
| Hat       | `StardewValley:Hat/<id>`                                                    |
| Object    | `StardewValley:Object/<id>`                                                 |
| Ring      | `StardewValley:Ring/<id>`                                                   |
| Tool      | `StardewValley:Tool/<type>` or `StardewValley:Tool/<type>/<quality number>` |
| Wallpaper | `StardewValley:Wallpaper/<id>`                                              |
| Weapon    | `StardewValley:Weapon/<id>`                                                 |

Not all key formats are listed. Also, other mods may add their own namespaces and key formats.

### Availability

Fish, trash, and treasure have availability information to determine when they can be found. These are the properties that are common to them all:

| Property           | Type       | Required | Default   | Description                                                                                                                    |
| ------------------ | ---------- | -------- | --------- | ------------------------------------------------------------------------------------------------------------------------------ |
| `BaseChance`       | `number`   | Yes      | N/A       | The base chance this will be caught. This is not a percentage chance, but rather a weight relative to all available entries.   |
| `StartTime`        | `integer`  | No       | 600       | Time this becomes available (inclusive).                                                                                       |
| `EndTime`          | `integer`  | No       | 2600      | Time this is no longer available (exclusive).                                                                                  |
| `Seasons`          | `array`    | No       | `["All"]` | Seasons this can be caught in. Default is all. ("Spring", "Summer", "Fall", "Winter", "All")                                   |
| `Weathers`         | `array`    | No       | `["All"]` | Weathers this can be caught in. Default is all. ("Sunny", "Rainy", "All")                                                      |
| `WaterTypes`       | `array`    | No       | `["All"]` | The type of water this can be caught in. Each location handles this differently. ("River", "PondOrOcean", "Freshwater", "All") |
| `MinFishingLevel`  | `integer`  | No       | 0         | Required fishing level to see this.                                                                                            |
| `MaxFishingLevel`  | `integer?` | No       | `null`    | Maximum fishing level required to see this, or null for no max.                                                                |
| `IncludeLocations` | `array`    | No       | `[]`      | List of locations this should be available in. (see below)                                                                     |
| `ExcludeLocations` | `array`    | No       | `[]`      | List of locations this should not be available in. This takes priority over `IncludeLocations`.                                |
| `Position`         | `object`   | No       | `{}`      | Bobber tile position constraints for the availability.                                                                         |
| `When`             | `object`   | No       | `{}`      | Content Patcher [conditions] for when this should be available.                                                                |

`IncludeLocations` and `ExcludeLocations` are arrays of location names. If `IncludeLocations` is empty, then it is assumed that all locations (except locations in `ExcludeLocations`) are valid. Additionally, `ExcludeLocations` takes priority over `IncludeLocations`. If a location appears in both arrays, then the item will not be available at that location.

Some locations have multiple names. For example, the mines have the location name "UndergroundMine", but each floor has the same location name. To specify which floor an item should be available on, use the name "UndergroundMine/N", where "N" is the floor number. Each floor can be referenced by either the name "UndergroundMine" or "UndergroundMine/N". This means that an item can be added to all floors in the mines by including it in the location "UndergroundMine", and can optionally be excluded from specific floors with "UndergroundMine/N".

Position constraints control where a bobber is allowed to be on a map for the item to be available:

| Property | Type      | Required | Default | Description                       |
| -------- | --------- | -------- | ------- | --------------------------------- |
| `X`      | `object?` | No       | `null`  | Constraints for the x-coordinate. |
| `Y`      | `object?` | No       | `null`  | Constraints for the y-coordinate. |

Coordinate constraints give a bit of flexibility on what coordinates are allowed:

| Property        | Type      | Required | Default | Description                                             |
| --------------- | --------- | -------- | ------- | ------------------------------------------------------- |
| `GreaterThan`   | `number?` | No       | `null`  | Coordinate value must be greater than this.             |
| `GreaterThanEq` | `number?` | No       | `null`  | Coordinate value must be greater than or equal to this. |
| `LessThan`      | `number?` | No       | `null`  | Coordinate value must be less than this.                |
| `LessThanEq`    | `number?` | No       | `null`  | Coordinate value must be less than or equal to this.    |

### Catch actions

Some actions can be taken whenever an item is caught. By combining these actions with Content Patcher conditions, some powerful conditions can be created. For example, a fish, trash, or treasure item can be configured to be caught only once.

| Property             | Type        | Required | Default | Description                                                                                                    |
| -------------------- | ----------- | -------- | ------- | -------------------------------------------------------------------------------------------------------------- |
| `CustomEvents`       | `string[]`  | No       | `[]`    | Raises custom events to be processed by a SMAPI mod.                                                           |
| `SetFlags`           | `string[]`  | No       | `[]`    | Sets mail flags. For custom flags, it is recommended to prefix them with your mod's ID (`You.YourMod/FlagId`). |
| `StartQuests`        | `integer[]` | No       | `[]`    | Sets one or more quests as active.                                                                             |
| `AddMail`            | `string[]`  | No       | `[]`    | Adds mail entries for the player's mail tomorrow.                                                              |
| `StartConversations` | `object`    | No       | `[]`    | Starts conversations. The key is the conversation ID and the value is the number of days.                      |

[namespaced key]: #Namespaced%20key
[availability info]: #Availability
[actions]: #Catch%20actions
[content schema]: /docs/TehPers.FishingOverhaul/schemas/contentPacks/content.schema.json
[conditions]: https://github.com/Pathoschild/StardewMods/blob/stable/ContentPatcher/docs/author-tokens-guide.md#conditions