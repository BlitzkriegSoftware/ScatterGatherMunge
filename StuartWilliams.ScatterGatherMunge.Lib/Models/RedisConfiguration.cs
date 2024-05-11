using Newtonsoft.Json;

namespace StuartWilliams.ScatterGatherMunge.Lib.Models
{

    /// <summary>
    /// REDIS Coniguration
    /// </summary>
    public class RedisConfiguration
    {
        /// <summary>
        /// Default: 127.0.0.1:6379 (admin)
        /// </summary>
        public const string RedisLocalHostDefault = "127.0.0.1:6379,allowAdmin=true";

        /// <summary>
        /// Connection String
        /// </summary>
        public string ConnectionString { get; set; } = RedisLocalHostDefault;

        /// <summary>
        /// Active DbIndex
        /// <para>Avoid Zero (0)</para>
        /// </summary>
        public int DbIndexActive { get; set; } = 0;

        /// <summary>
        /// Is connection configuration valid
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(this.ConnectionString);
            }
        }

        /// <summary>
        /// From a config key/value pair set the correct property
        /// </summary>
        /// <param name="key">(sic)</param>
        /// <param name="value">(sic)</param>
        public void SetProperty(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key)) return;

            switch (key.ToLowerInvariant())
            {
                case "redis-connection": this.ConnectionString = value; break;
                case "redis-dbindex": this.DbIndexActive = int.Parse(value); break;
            }
        }

        /// <summary>
        /// Debug String
        /// </summary>
        /// <returns>Debug String</returns>
        public override string ToString()
        {
            return $"{this.ConnectionString}; dbIndex: {this.DbIndexActive}";
        }

        /// <summary>
        /// To JSON
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }

}
