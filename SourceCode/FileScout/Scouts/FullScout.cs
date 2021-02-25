using FileScout.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace FileScout.Scouts
{
    /// <summary>
    /// 実装されている全機能の調査を提供します。
    /// </summary>
    public class FullScout : BaseScout
    {
        /// <summary>
        /// <see cref="FullScout"/> クラスの新しいインスタンスを作成します。
        /// </summary>
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
                if (type.IsAbstract | type == GetType() | type.GetInterface("IScout") != typeof(IScout))
                {
                    continue;
                }

                // 調査項目のマージ
                var scout = (IScout)Activator.CreateInstance(type);
                ScoutingMethod =
                    ScoutingMethod.Concat(scout.ScoutingMethod)
                    .GroupBy(x => x.Key, (_, x) => x.First())
                    .ToDictionary(x => x.Key, x => x.Value);
            }
        }
    }
}
