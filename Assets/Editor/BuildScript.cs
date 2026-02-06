using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Список сцен
        // ========================
        string[] scenes = {
        "Assets/Scenes/Game.unity",
        };

        // ========================
        // Пути к файлам сборки
        // ========================
        string aabPath = "TowerStack.aab";
        string apkPath = "TowerStack.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64 = "MIIJ1AIBAzCCCX4GCSqGSIb3DQEHAaCCCW8EgglrMIIJZzCCBa4GCSqGSIb3DQEHAaCCBZ8EggWbMIIFlzCCBZMGCyqGSIb3DQEMCgECoIIFQDCCBTwwZgYJKoZIhvcNAQUNMFkwOAYJKoZIhvcNAQUMMCsEFC09akPWtNHG7K3ziDXqYYDOBrSYAgInEAIBIDAMBggqhkiG9w0CCQUAMB0GCWCGSAFlAwQBKgQQaeYIvP++ugYDsMw+81NXmASCBNCu7F9JoVStdN65pEupAR0/Gh3j95mrS6T3m3Kov+hEg5+upVI9dUMkgiYOed5v72EAsxSbgEWUZ/oHYucy8hjP7zYr2+6cHcP2FcZpjDNMb0wZKGIUb4+OQ6o50mLoTjDUOaIpwXu3qDIq/s0D/bn1PEYCFwnMBL3uuDhHcW/nEgLVKFwXboT08RTYhM8J3ieBcV8UOKpC7S0VNsAzV9dwQ7UcuPw8/07fxfnqdIIma3YQPjHuF6ksTfLIki7pjjTFy2IeSosV1FebTJTQEIG9xRYTyMcERV4JgYotCTrrjnAK/1Rvc2f1i7ORKjCfADHUYu0o1wjITOXjkOqWqsAFmpNSRjD0BsnFsdUR0HbJzpSb91S4ki4ORQP6cB7EH5eb5AZ9Fie731A1CdjjAq/uW21x8GdRuychTOlLum628D+bJZSFiW3A/GfwwCh5hjtO+rF8d71zJdyltt9eVXT7OYS41mloAmONAlI7ddw6l7laVoJplq4KDM04RJms8ka0BbkTNuMsmErs+BMwAdxUF/jh407yVS3GLWfmnwgP74a4TN/qxAzPbUXmhAgieO/yKwLgHZGfNJMsmyHPyIrFo9EvdQ2xxm+hl2JgsdVZrLoUvCHkTA2nlnuw/9ccadBtgaopOjwm37g0JbXT+Gz9XpRaHI/O4E9fjSK6v/873VmRpbMBNxqO0jyYZ/8wU9lie/jkjq0oHpLCn4cb2MFPjtAwqLFeNAKVWG8ai3/fn0Zi5l32DMRvd6Z+fXFRTuYhhX4/x9REo02UartCSnogSWbyncKVB9oHJPqhzgPsIvfSQ6XQxBnpxbaNJjhSttC//PphEPkdxG6w0X9Q50TpDI9dbSPSAaCr1yDq62jP5DxsiSEUeSpfBOnqDKsyvh7CP/BDgHUIX4pDhcvubbIvTxG11O1PFASxG1305rYi82ov+ZCxl2wzj5rXpQyf0LHF4LQYdj3OzTF/+4NZsXQYRJxxhzAvLvkwlUPjbPeww17EgL+ZgDeD4GbezbGRfndgkCNB2ynv3YTeCspoZ/GPHfm7JYkOnwTeiORH0GTVGUgMm4arL6AhVNoYvkO9tOgTpQCwSStUQ/+ofxLbcYl8I+gfwaUv9afKfMNhrdMKXGbm7JdHa53RuBN0X7shm0sZjtvCT4i6YgJVrsYidWVfGzp+8NWD3Vu+AuI3aish594iyfoVdBu4E/CMpVDcF53sU20J7Qp6nWZcSnQUPng8JDGrYVpzfvLtcF26uxeKy0OEvG4CrPE6FAYFdwYtAJ7DX8P8CLjSgNeFESVY5UWGf7GXJypmrX5p4Ev2DDuvzjHGzsvZAOBQTAaZNc8x7dGoIUMU5AiTR5N4OvfVYFH6JzdLn+gLn5qJ8swxzHnYiy9RMc/cYNLe+fdh6bzU2J5zgX5E8e182CLadpQny3dJxrCCqaFSC2hGNGb+O9o+iMfxTLjfCG0U4Ntzqu1/r3NZkf45fvox+kWksKxc21FKUA7X1ZK8Re0Jy+ENHj+UtY1KYUAxv2HCvkXOoLGneyDLceKPmqo3EYudgT+0+mmhuOSQpqHWjh+vBRLxM1mQbk9hojAciqvMc48sXI6Jb5o0IwnHkwMkdoBxxc2Zl2hj/GRMkEOcOXanzjZfszI4RTFAMBsGCSqGSIb3DQEJFDEOHgwAaABlAGwAcABlAHIwIQYJKoZIhvcNAQkVMRQEElRpbWUgMTc3MDIxNjEyNzY0NTCCA7EGCSqGSIb3DQEHBqCCA6IwggOeAgEAMIIDlwYJKoZIhvcNAQcBMGYGCSqGSIb3DQEFDTBZMDgGCSqGSIb3DQEFDDArBBTWUwi0fCZrQdKzNK5JthibkPLGeAICJxACASAwDAYIKoZIhvcNAgkFADAdBglghkgBZQMEASoEEIXvfMF+5n77u9pqP4gtT6OAggMgUzBUeeQKl3vfKX9D5W1dEwcvKftd2e+xI1/eN0mYIwyodH5P34NRkZUO/muu7nvaZuwiWRn/MgbiL+pf1v9BKh4YNVhS4ziIQrjkLZut24hlZdBc+5CLucWsA+L4tBqJCjLGUnbVQzwDSWrBm9qSq8XKVCxM5ifx1YvQoAF29u09U4ymqwaq/0VRuDlB7JABXkZfnFGeOJgjx+fcVhZUH5yPNZbj77vI1A7eVi7BdcucxERYvieLgtYj2cqDfdHKoEyt3Rlpxvq2LBMSjjE1vAWPJtBCjs+qD3udL6FTc10ArEuqxOE1U0M/Gmq4b4YULghqSLMhN+gYUoPcUHl+McdbO1tGrSUmYCiJzKaWiZpf3dBKwaKR4jcce5Dr/oEYWfBgSi3Nh+sxRvHSy2W+MxL2yXkQDMiJhVygc8/0FUhhPbI9UYpLcfJm+YBV0U9kQmTK+QS20xzbn8pm9B3n7AQbJCEOed2/F6Le4UKWKmAzbU+u/pLGUC2f2r7JIsZC76mHO5QenAo4UGLvj1x847buNQOiIbbosq31NZyB7qUIHf9C5VRcdDUSrpyhNo2mkspg3xyO7k1NAjbHg0Ic/9GRkKPrt7FxGDY8Eh9efSkLpXu7Wp9hXK7eVoKPdb0na25DfINkSR3wrfl5fgFB72i3QIOc384SCTDZaauTVU4klqKlprlTTXzqDNECFnHlX9CmyCb9lwMRb/u0wdLk5x71ciNImx9nwPnAq/dAIyKzvkci/4C+1o5jR4TzXSg1FwAfi6NDjLOwvGG41yDw/OF0hhsSsYGFcoGGVlo1i2iHnlBN4kYXv178iSuRzHzsJbwpWOGRMohySzScGmOsci7Jq/dQR0RUC7FpNbUn5VPNJOS4lCcNMGUDyf3HQ0CQUr8c4ftbM25TpcC7kdyCKVtFbb3uBgI+Moil6Me6lvbh+ovJ2SYD9Cw2vV74XoaII7hOjpj/NLc0w5kCA3MEva1M/DmXBJ4GeQJUhqP5Z6M5q4VTZjTxNA0f+gntMKrn8jyXLRzi0tEx5CkV0gUzWLYeNWo8ql/eOu862bST724wTTAxMA0GCWCGSAFlAwQCAQUABCDeFPpzhvAOSgxVI39KzDXyWwgTrZogCpH1Il0DXO/FKwQUguIoIM068BE9T5mPi6Z4BsY+JVwCAicQ";
        string keystorePass = "comer27";
        string keyAlias = "helper";
        string keyPass = "comer27";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
{
    // Удаляем пробелы, переносы строк и BOM
    string cleanedBase64 = keystoreBase64.Trim()
                                         .Replace("\r", "")
                                         .Replace("\n", "")
                                         .Trim('\uFEFF');

    // Создаем временный файл keystore
    tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
    File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(cleanedBase64));

    PlayerSettings.Android.useCustomKeystore = true;
    PlayerSettings.Android.keystoreName = tempKeystorePath;
    PlayerSettings.Android.keystorePass = keystorePass;
    PlayerSettings.Android.keyaliasName = keyAlias;
    PlayerSettings.Android.keyaliasPass = keyPass;

    Debug.Log("Android signing configured from Base64 keystore.");
}
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Общие параметры сборки
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Сборка AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Сборка APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Удаление временного keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}
