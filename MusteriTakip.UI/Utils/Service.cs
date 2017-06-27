﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusteriTakip.UI.Utils
{
    public static class Service<T>
    {
        public static long AddOrUpdate(T Entity)
        {
            long Id = Convert.ToInt64(Entity.GetType().GetProperties().FirstOrDefault(p => p.Name == "Id").GetValue(Entity));
            if (Id == 0)
            {
                var list = SelectAll();
                long GivenId = DateTime.Now.Ticks;
                Entity.GetType().GetProperties().FirstOrDefault(p => p.Name == "Id").SetValue(Entity, GivenId);
                Entity.GetType().GetProperties().FirstOrDefault(p => p.Name == "DateCreated").SetValue(Entity, DateTime.Now);
                list.Add(Entity);
                Save(list);
                return GivenId;
            }
            else
            {
                var list = SelectAll();
                foreach (var EntProp in Entity.GetType().GetProperties())
                {
                    var obj = list.FirstOrDefault(s => Convert.ToInt64(s.GetType().GetProperty("Id").GetValue(s)) == Id);
                    foreach (var DataProp in obj.GetType().GetProperties())
                    {
                        if (DataProp.Name == EntProp.Name)
                        {
                            var value = EntProp.GetValue(Entity);
                            DataProp.SetValue(list.FirstOrDefault(s => Convert.ToInt64(s.GetType().GetProperty("Id").GetValue(s)) == Id), value);
                        }
                    }
                }

                Save(list);
                return Id;
            }
        }

        public static void Delete(long Id)
        {
            var list = SelectAll();
            list.Remove(list.First(s => Convert.ToInt64(s.GetType().GetProperties().FirstOrDefault(p => p.Name == "Id").GetValue(s)) == Id));
            Save(list);
        }

        public static T Select(long Id)
        {
            return SelectAll().FirstOrDefault(s => Convert.ToInt64(s.GetType().GetProperties().FirstOrDefault(p => p.Name == "Id").GetValue(s)) == Id);
        }

        public static List<T> SelectAll()
        {

            string json = File.ReadAllText(Properties.Settings.Default.DBDir + "\\" + typeof(T).Name + "s.json");
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(json).ToList();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }
        public static BindingList<T> toBindingList
        {
            get
            {
                BindingList<T> entities = new BindingList<T>();
                foreach (T t in SelectAll())
                {
                    entities.Add(t);
                }
                return entities;
            }
        }
        public static void Save(List<T> List)
        {
            string json = JsonConvert.SerializeObject(List);
            File.WriteAllText(Properties.Settings.Default.DBDir + "\\" + typeof(T).Name + "s.json", json);

        }
    }
}
