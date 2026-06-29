using HarmonyLib;

namespace LanternPickup
{
    internal static class LanternPickupPatches
    {
        private const string PickupCommandName = "take";

        private static readonly BlockActivationCommand[] PickupCommands =
        {
            new BlockActivationCommand(PickupCommandName, "hand", _enabled: true)
        };

        private static bool IsTargetLantern(BlockValue blockValue)
        {
            var blockName = blockValue.Block?.GetBlockName();
            switch (blockName)
            {
                case "lanternDecorLightWhite":
                case "lanternDecorLightRed":
                case "lanternDecorLightYellow":
                case "lanternDecorLightGreen":
                case "lanternDecorLightBlue":
                case "lanternDecorLightGrey":
                case "lanternDecorLightBlack":
                case "lanternDecorLightRandomHelper":
                case "lanternOld_01":
                case "lanternOld_02":
                case "lanternOld_03":
                case "lanternOld_04":
                    return true;
                default:
                    return false;
            }
        }

        private static string GetPickupDisplayName(Block block)
        {
            var itemName = block.GetBlockName();
            if (!string.IsNullOrEmpty(block.PickedUpItemValue))
            {
                itemName = block.PickedUpItemValue;
                var commaIndex = itemName.IndexOf(',');
                if (commaIndex >= 0)
                {
                    itemName = itemName.Substring(0, commaIndex);
                }
            }
            else if (!string.IsNullOrEmpty(block.PickupTarget))
            {
                itemName = block.PickupTarget;
            }

            return Localization.Get(itemName);
        }

        private static bool TryPickup(WorldBase world, Vector3i blockPos, BlockValue blockValue, EntityPlayerLocal player)
        {
            if (world == null || player == null)
            {
                return false;
            }

            var persistentPlayer = world.GetGameManager().GetPersistentLocalPlayer();
            if (!world.CanPickupBlockAt(blockPos, persistentPlayer))
            {
                player.PlayOneShot("keystone_impact_overlay");
                return false;
            }

            var itemStack = blockValue.Block.OnBlockPickedUp(world, blockPos, blockValue, player.entityId);
            if (!player.inventory.CanTakeItem(itemStack) && !player.bag.CanTakeItem(itemStack))
            {
                GameManager.ShowTooltip(player, Localization.Get("xuiInventoryFullForPickup"), string.Empty, "ui_denied");
                return false;
            }

            world.GetGameManager().PickupBlockServer(blockPos, blockValue, player.entityId);
            return true;
        }

        [HarmonyPatch(typeof(BlockLight), nameof(BlockLight.GetActivationText))]
        private static class GetActivationTextPatch
        {
            private static bool Prefix(
                WorldBase _world,
                BlockValue _blockValue,
                Vector3i _blockPos,
                EntityAlive _entityFocusing,
                ref string __result)
            {
                if (!IsTargetLantern(_blockValue))
                {
                    return true;
                }

                if (!_world.CanPickupBlockAt(_blockPos, _world.GetGameManager().GetPersistentLocalPlayer()))
                {
                    __result = null;
                    return false;
                }

                __result = string.Format(Localization.Get("pickupPrompt"), GetPickupDisplayName(_blockValue.Block));
                return false;
            }
        }

        [HarmonyPatch(typeof(BlockLight), nameof(BlockLight.HasBlockActivationCommands))]
        private static class HasBlockActivationCommandsPatch
        {
            private static bool Prefix(BlockValue _blockValue, ref bool __result)
            {
                if (!IsTargetLantern(_blockValue))
                {
                    return true;
                }

                __result = true;
                return false;
            }
        }

        [HarmonyPatch(typeof(BlockLight), nameof(BlockLight.GetBlockActivationCommands))]
        private static class GetBlockActivationCommandsPatch
        {
            private static bool Prefix(BlockValue _blockValue, ref BlockActivationCommand[] __result)
            {
                if (!IsTargetLantern(_blockValue))
                {
                    return true;
                }

                PickupCommands[0].enabled = true;
                __result = PickupCommands;
                return false;
            }
        }

        [HarmonyPatch(typeof(BlockLight), nameof(BlockLight.OnBlockActivated))]
        private static class OnBlockActivatedPatch
        {
            private static bool Prefix(
                string _commandName,
                WorldBase _world,
                Vector3i _blockPos,
                BlockValue _blockValue,
                EntityPlayerLocal _player,
                ref bool __result)
            {
                if (!IsTargetLantern(_blockValue) || _commandName != PickupCommandName)
                {
                    return true;
                }

                __result = TryPickup(_world, _blockPos, _blockValue, _player);
                return false;
            }
        }
    }
}
