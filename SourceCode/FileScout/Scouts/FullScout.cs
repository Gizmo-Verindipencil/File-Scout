using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FileScout.Interfaces;

namespace FileScout.Scouts
{
    /// <summary>
    /// 全てを調査する斥候。
    /// </summary>
    public class FullScout : BaseScout
    {
        public FullScout()
        {
            /// 実装されている全てのScoutの調査項目を含める
            var types = 
                Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => string.Equals(x.Namespace, "FileScout.Scouts", StringComparison.Ordinal))
                .ToArray();

            foreach (var type in types)
            {
                // 抽象クラス・このクラスの場合は何もしない
                if (type.IsAbstract | type == GetType() | !type.IsSubclassOf(typeof(BaseScout)))
                {
                    continue;
                }

                // クラスの調査項目を取得
                var scout = (BaseScout)Activator.CreateInstance(type);
                var field = type.GetField("Methods", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance);
                var unknown = (Dictionary<string, IScoutingMethod>)field.GetValue(scout);

                // 調査項目のマージ
                Methods = 
                    Methods.Concat(unknown)
                    .GroupBy(x => x.Key, (_, x) => x.First())
                    .ToDictionary(x => x.Key, x => x.Value);
            }
        }
    }
}
