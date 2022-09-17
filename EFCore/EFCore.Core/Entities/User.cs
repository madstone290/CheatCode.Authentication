using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Core.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
        public string Color { get; set; }
        public List<string> Tags { get; set; } = new();


        public User(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 위치명을 변경한다.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="DomainException"></exception>
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("위치명이 비었습니다");
            Name = name.Trim();
        }
    }
}
