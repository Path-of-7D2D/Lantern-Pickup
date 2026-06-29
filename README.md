# Lantern Pickup

Lantern Pickup is a small XML-only 7 Days To Die V3.0 mod that lets world
lanterns be picked up directly instead of forcing you to scrap them and craft a
replacement.

## Features

- POI camping lanterns can be picked up directly.
- POI old lanterns can be picked up directly.
- Picked-up camping lanterns become the normal player-placeable Lantern item.
- Picked-up old lanterns become the normal player-placeable Old Lantern item.
- No DLL and no Easy Anti-Cheat skip requirement.

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

Install the mod on the server. Clients should not need a local copy because this
mod only patches server-side block configuration.

## Compatibility

This mod patches `blocks.xml` entries for:

- `lanternDecorLightWhite`, the base block used by the POI camping lantern color
  variants.
- `lanternOld_01`, the base block used by the POI old lantern variants.

It may conflict with another mod that changes pickup behavior for the same
blocks.

## License

This project is licensed under the [MIT License](LICENSE).

