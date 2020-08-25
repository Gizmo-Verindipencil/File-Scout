using System;
using System.Linq;
using System.Reflection;

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

                // 調査項目のマージ
                var scout = (BaseScout)Activator.CreateInstance(type);
                Methods = 
                    Methods.Concat(scout.Methods)
                    .GroupBy(x => x.Key, (_, x) => x.First())
                    .ToDictionary(x => x.Key, x => x.Value);
            }
        }
    }
}
