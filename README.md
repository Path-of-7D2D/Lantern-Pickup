# Lantern Pickup

Lantern Pickup is a small 7 Days To Die V3.0 mod that lets world lanterns be
picked up directly instead of forcing you to scrap them and craft a replacement.

## Features

- POI camping lanterns can be picked up directly.
- POI old lanterns can be picked up directly.
- Picked-up camping lanterns become the normal player-placeable Lantern item.
- Picked-up old lanterns become the normal player-placeable Old Lantern item.
- Adds the normal pickup prompt to POI light blocks.
- Allows pickup even if the lantern was damaged while testing.

## Installation

1. Download the latest `LanternPickup-*.zip` from the
   [GitHub Releases](https://github.com/Path-of-7D2D/Lantern-Pickup/releases)
   page.
2. Extract the release zip.
3. Copy the `1A-LanternPickup` folder into your `Mods` folder:

```text
7 Days To Die/Mods/1A-LanternPickup/
```

The folder is installed correctly when this file exists:

```text
7 Days To Die/Mods/1A-LanternPickup/ModInfo.xml
```

## Multiplayer

Install the mod on the server and every connecting client. The pickup prompt is
client-side UI, and the pickup action is implemented by `LanternPickup.dll`.

## Easy Anti-Cheat

This mod uses Harmony patches, so Easy Anti-Cheat must be off. The mod is marked
with `SkipWithAntiCheat`.

## Compatibility

This mod patches `blocks.xml` entries for:

- `lanternDecorLightWhite`, the base block used by the POI camping lantern color
  variants.
- `lanternOld_01`, the base block used by the POI old lantern variants.

It may conflict with another mod that changes pickup behavior for the same
blocks or patches `BlockLight` activation.

## License

This project is licensed under the [MIT License](LICENSE).
