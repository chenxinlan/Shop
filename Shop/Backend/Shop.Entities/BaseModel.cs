using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Entities
{
    public abstract class BaseModel
    {
        public BaseModel() { }

        private string _id;

        public string id
        {
            get { return _id; }
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                {
                    _id = Guid.NewGuid().ToString("N");
                }
                else
                {
                    _id = value;
                }
            }
        }

        /// <summary>
        /// 访问IP
        /// </summary>
        public string visitIP { get; set; }

        /// <summary>
        /// 服务器IP
        /// </summary>
        public string serverIP { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime operateTime { get; set; } = DateTime.Now;//默认值
    }
}
