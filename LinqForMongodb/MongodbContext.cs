﻿using MongoDB.Driver;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace LinqForMongodb.Context
{
    /// <summary>
    /// Mongodb数据库连接上下文
    /// </summary>
    public class MongodbContext:IDisposable
    {
        /// <summary>
        /// 实例化Mongodb数据库连接
        /// </summary>
        /// <param name="conn">连接字符串</param>
        public MongodbContext(string conn,string dbname)
        {
            this.ConnectString = conn;
            this.DbName = dbname;
            this.mongoClient = new MongoClient(this.ConnectString);
            this.Database = mongoClient.GetDatabase(this.DbName);
        }

        /// <summary>
        /// MongoDb数据库客户端
        /// </summary>
        public MongoClient mongoClient { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectString { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName { get; set; }

        public IMongoDatabase Database { get; set; }

        public IQueryable<T> Table<T>()
        {
            //return new Dbset<T>(new MongoDbQueryProvider<T>(new MongoProvider(Database)));
            return Database.GetCollection<T>(ReflectionHelper.GetScrubbedGenericName(typeof(T))).AsQueryable();
        }
        
        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                this.mongoClient = null;

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~MongodbContext() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
