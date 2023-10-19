using System.IO;
using UnityEngine;

namespace Constants
{
  public static class GameConstants
  {
      public static readonly string GameSceneName = "Game";

      public static readonly string SaveKey = "Player";
      public static readonly string PathToWalletDisplayerPrefab = "UI/WalletDisplayer";

      public static readonly string PathToSaveFolder = Path.Combine(Application.persistentDataPath, "Save");
      public static readonly string PathToSaveFile = Path.Combine(PathToSaveFolder, "save.txt");
  }
}