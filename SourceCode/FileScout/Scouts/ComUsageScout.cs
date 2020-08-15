﻿using FileScout.ScoutsingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// COMオブジェクト生成回数を調査する斥候。
    /// </summary>
    public class ComUsageScout : TextScout
    {
        public ComUsageScout()
        {
            /// COM調査項目の追加
            void addMethod(string comObjectName)
            {
                var method = new NumberOfComInstanceCreationScoutingMethod() { ComObjectName = comObjectName };
                Methods.Add(key: $"COM({comObjectName})", value: method);
            }

            /// 調査項目の定義
            addMethod("Access.Application");
            addMethod("Excel.Application");
            addMethod("Shell.Application");
            addMethod("basp21");
            addMethod("basp21.FTP");
            addMethod("ADODB.Command");
            addMethod("ADODB.Connection");
            addMethod("ADODB.Recordset");
            addMethod("WScript.Network");
            addMethod("Scripting.FileSystemObject");
        }
    }
}
