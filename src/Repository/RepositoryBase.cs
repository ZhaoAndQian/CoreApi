using Microsoft.EntityFrameworkCore;
using Preoff.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Preoff.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class, new()
    {
        private PreoffContext _dbContext;
        public RepositoryBase(PreoffContext _db)
        {
            _dbContext = _db;
        }

        /// <summary>
        /// 公用泛型处理属性
        /// 注:所有泛型操作的基础
        /// </summary>
        public DbSet<T> DbSet
        {
            get { return this._dbContext.Set<T>(); }
        }

        #region 获取单条记录
        /// <summary>
        /// 通过lambda表达式获取一条记录p=>p.id==id
        /// </summary>
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return DbSet.AsNoTracking().SingleOrDefault(predicate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 增删改操作

        /// <summary>
        /// 添加一条模型记录，自动提交更改
        /// </summary>
        public virtual bool Save(T entity)
        {
            try
            {
                int row = 0;
                var entry = _dbContext.Entry<T>(entity);
                entry.State = EntityState.Added;
                row = _dbContext.SaveChanges();
                entry.State = EntityState.Detached;
                return row > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 添加一条模型记录,返回模型对应值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int SaveGetId(T entity)
        {
            try
            {
                var entry = _dbContext.Entry<T>(entity);
                entry.State = EntityState.Added;
                _dbContext.SaveChanges();
                entry.State = EntityState.Detached;
                int getid=Convert.ToInt32(entity.GetType().GetProperty("Id").GetValue(entity));
                return getid;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        /// <summary>
        /// 更新一条模型记录，自动提交更改
        /// </summary>
        public virtual bool Update(T entity)
        {
            try
            {
                int rows = 0;
                var entry = _dbContext.Entry(entity);
                entry.State = EntityState.Modified;
                rows = _dbContext.SaveChanges();
                entry.State = EntityState.Detached;
                return rows > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 更新模型记录，如不存在进行添加操作
        /// </summary>
        public virtual bool SaveOrUpdate(T entity, bool isEdit)
        {
            try
            {
                return isEdit ? Update(entity) : Save(entity);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// 删除一条或多条模型记录，含事务
        /// </summary>
        public virtual int Delete(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                int rows = 0;
                IQueryable<T> entry = (predicate == null) ? DbSet.AsQueryable() : DbSet.Where(predicate);
                List<T> list = entry.ToList();
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        DbSet.Remove(list[i]);
                    }
                    rows = _dbContext.SaveChanges();
                }
                return rows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 使用原始SQL语句,含事务处理
        /// </summary>
        public virtual int DeleteBySql(string sql, params DbParameter[] para)
        {
            try
            {
                return _dbContext.Database.ExecuteSqlCommand(sql, para);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 多模型操作

        /// <summary>
        /// 增加多模型数据，指定独立模型集合
        /// </summary>
        public virtual int SaveList<T1>(List<T1> t) where T1 : class
        {
            try
            {
                if (t == null || t.Count == 0) return 0;
                _dbContext.Set<T1>().Local.Clear();
                foreach (var item in t)
                {
                    _dbContext.Set<T1>().Add(item);
                }
                return _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 增加多模型数据，与当前模型一致
        /// </summary>
        public virtual int SaveList(List<T> t)
        {
            try
            {
                DbSet.Local.Clear();
                foreach (var item in t)
                {
                    DbSet.Add(item);
                }
                return _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 更新多模型，指定独立模型集合
        /// </summary>
        public virtual int UpdateList<T1>(List<T1> t) where T1 : class
        {
            if (t.Count <= 0) return 0;
            try
            {
                foreach (var item in t)
                {
                    _dbContext.Entry<T1>(item).State = EntityState.Modified;
                }
                return _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 更新多模型，与当前模型一致
        /// </summary>
        public virtual int UpdateList(List<T> t)
        {
            if (t.Count <= 0) return 0;
            try
            {
                foreach (var item in t)
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                }
                return _dbContext.SaveChanges();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// 批量删除数据，当前模型
        /// </summary>
        public virtual int DeleteList(List<T> t)
        {
            if (t == null || t.Count == 0) return 0;
            foreach (var item in t)
            {
                if (DbSet.Contains(item)) {
                    DbSet.Remove(item);
                }
            }
            return _dbContext.SaveChanges();
        }
        /// <summary>
        /// 批量删除数据，自定义模型
        /// </summary>
        public virtual int DeleteList<T1>(List<T1> t) where T1 : class
        {
            try
            {
                if (t == null || t.Count == 0) return 0;
                foreach (var item in t)
                {
                    _dbContext.Set<T1>().Remove(item);
                }
                return _dbContext.SaveChanges();
            }
            catch (Exception e) { throw e; }
        }
        #endregion

        #region 存储过程操作
        /// <summary>
        /// 执行返回影响行数的存储过程
        /// </summary>
        /// <param name="procname">过程名称</param>
        /// <param name="parameter">参数对象</param>
        /// <returns></returns>
        public virtual object ExecuteProc(string procname, params DbParameter[] parameter)
        {
            try
            {
                return _dbContext.Database.ExecuteSqlCommand(procname, parameter);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// 执行返回结果集的存储过程
        /// </summary>
        /// <param name="procname">过程名称</param>
        /// <param name="parameter">参数对象</param>
        /// <returns></returns>
        public virtual object ExecuteQueryProc(string procname, params DbParameter[] parameter)
        {
            try
            {
                return _dbContext.Set<T>().FromSql(procname, parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 存在验证操作
        /// <summary>
        /// 验证当前条件是否存在相同项
        /// </summary>
        public virtual bool IsExist(Expression<Func<T, bool>> predicate)
        {
            var entry = DbSet.Where(predicate);
            return (entry.Any());
        }

        /// <summary>
        /// 根据SQL验证实体对象是否存在
        /// </summary>
        public virtual bool IsExist(string sql, params DbParameter[] para)
        {
            //IEnumerable result = _dbContext.Database.SqlQuery(typeof(int), sql, para);

            //if (result.GetEnumerator().Current == null || result.GetEnumerator().Current.ToString() == "0")
            //    return false;
            return true;
        }
        #endregion

        #region 获取多条数据操作
        /// <summary>
        /// 返回IQueryable集合，延时加载数据
        /// </summary>
        public virtual IQueryable<T> LoadAll(Expression<Func<T, bool>> predicate=null)
        {
            try
            {
                if (predicate != null)
                {
                    return DbSet.Where(predicate).AsNoTracking<T>();
                }
                return DbSet.AsQueryable<T>().AsNoTracking<T>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 返回List集合,不采用延时加载
        /// </summary>
        public virtual List<T> LoadListAll(Expression<Func<T, bool>> predicate=null)
        {
            try
            {
                if (predicate != null)
                {
                    return DbSet.Where(predicate).AsNoTracking().ToList();
                }
                return DbSet.AsQueryable<T>().AsNoTracking().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 返回IEnumerable集合，采用原始T-Sql方式
        /// </summary>
        public virtual IEnumerable<T> LoadEnumerableAll(string sql, params DbParameter[] para)
        {
            //try
            //{
            //    return _dbContext.Database.SqlQuery<T>(sql, para);
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            return null;
        }
        /// <summary>
        /// 返回IEnumerable动态集合，采用原始T-Sql方式
        /// </summary>
        public virtual System.Collections.IEnumerable LoadEnumerable(string sql, params DbParameter[] para)
        {
            //try
            //{
            //    return _dbContext.Database.SqlQueryForDynamic(sql, para);
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            return null;
        }
        /// <summary>
        /// 返回IList集合，采用原始T-Sql方式
        /// </summary>
        public virtual List<T> SelectBySql(string sql, params DbParameter[] para)
        {
            //try
            //{
            //    return _dbContext.Database.SqlQuery(typeof(T), sql, para).Cast<T>().ToList();
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            return null;
        }
        /// <summary>
        /// 指定泛型，返回IList集合，采用原始T-Sql方式
        /// </summary>
        public virtual List<T1> SelectBySql<T1>(string sql, params DbParameter[] para)
        {
            //try
            //{
            //    return _dbContext.Database.SqlQuery<T1>(sql, para).ToList();
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            return null;
        }
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <typeparam name="TResult">数据结果，与TEntity一致</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>实体集合</returns>
        public virtual List<TResult> QueryEntity<TEntity, TOrderBy, TResult>
            (Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Expression<Func<TEntity, TResult>> selector,
            bool IsAsc)
            where TEntity : class
            where TResult : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return query.Cast<TResult>().AsNoTracking().ToList();
            }
            return query.Select(selector).AsNoTracking().ToList();
        }

        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回Object对象
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>自定义实体集合</returns>
        public virtual List<object> QueryObject<TEntity, TOrderBy>
            (Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Func<IQueryable<TEntity>,
            List<object>> selector,
            bool IsAsc)
            where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return query.AsNoTracking().ToList<object>();
            }
            return selector(query);
        }

        #endregion

        #region 分页操作
        /// <summary>
        /// 待自定义分页函数，使用必须重写，指定数据模型
        /// </summary>
        public virtual IList<T1> PageByListSql<T1>(string sql, IList<DbParameter> parameters, PageCollection page)
        {
            return null;
        }
        /// <summary>
        /// 待自定义分页函数，使用必须重写，
        /// </summary>
        public virtual IList<T> PageByListSql(string sql, IList<DbParameter> parameters, PageCollection page)
        {
            return null;
        }

        /// <summary>
        /// 对IQueryable对象进行分页逻辑处理，过滤、查询项、排序对IQueryable操作
        /// </summary>
        /// <param name="t">Iqueryable</param>
        /// <param name="index">当前页</param>
        /// <param name="PageSize">每页显示多少条</param>
        /// <returns>当前IQueryable to List的对象</returns>
        public virtual PageInfo<T> Query(IQueryable<T> query, int index, int PageSize)
        {
            if (index < 1)
            {
                index = 1;
            }
            if (PageSize <= 0)
            {
                PageSize = 20;
            }
            int count = query.Count();

            int maxpage = count / PageSize;

            if (count % PageSize > 0)
            {
                maxpage++;
            }
            if (index > maxpage)
            {
                index = maxpage;
            }
            if (count > 0)
                query = query.Skip((index - 1) * PageSize).Take(PageSize);
            return new PageInfo<T>(index, PageSize, count, query.ToList());
        }
        /// <summary>
        /// 通用EF分页，默认显示20条记录
        /// </summary>
        /// <typeparam name="TEntity">实体模型</typeparam>
        /// <typeparam name="TOrderBy">排序类型</typeparam>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">显示条数</param>
        /// <param name="where">过滤条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">结果集合</param>
        /// <param name="isAsc">排序方向true正序 false倒序</param>
        /// <returns>自定义实体集合</returns>
        public virtual PageInfo<object> Query<TEntity, TOrderBy>
            (int index, int pageSize,
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Func<IQueryable<TEntity>,
            List<object>> selector,
            bool isAsc)
            where TEntity : class
        {
            if (index < 1)
            {
                index = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 20;
            }

            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }
            int count = query.Count();

            int maxpage = count / pageSize;

            if (count % pageSize > 0)
            {
                maxpage++;
            }
            if (index > maxpage)
            {
                index = maxpage;
            }

            if (orderby != null)
            {
                query = isAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (count > 0)
                query = query.Skip((index - 1) * pageSize).Take(pageSize);
            //返回结果为null，返回所有字段
            if (selector == null)
                return new PageInfo<object>(index, pageSize, count, query.ToList<object>());
            return new PageInfo<object>(index, pageSize, count, selector(query).ToList());
        }
        ///// <summary>
        ///// 普通SQL查询分页方法
        ///// </summary>
        ///// <param name="index">当前页</param>
        ///// <param name="pageSize">显示行数</param>
        ///// <param name="tableName">表名/视图</param>
        ///// <param name="field">获取项</param>
        ///// <param name="filter">过滤条件</param>
        ///// <param name="orderby">排序字段+排序方向</param>
        ///// <param name="group">分组字段</param>
        ///// <returns>结果集</returns>
        //public virtual PageInfo Query(int index, int pageSize, string tableName, string field, string filter, string orderby, string group, params DbParameter[] para)
        //{
        //    //执行分页算法
        //    if (index <= 0)
        //        index = 1;
        //    int start = (index - 1) * pageSize;
        //    if (start > 0)
        //        start -= 1;
        //    else
        //        start = 0;
        //    int end = index * pageSize;

        //    #region 查询逻辑
        //    string logicSql = "SELECT";
        //    //查询项
        //    if (!string.IsNullOrEmpty(field))
        //    {
        //        logicSql += " " + field;
        //    }
        //    else
        //    {
        //        logicSql += " *";
        //    }
        //    logicSql += " FROM (" + tableName + " ) where";
        //    //过滤条件
        //    if (!string.IsNullOrEmpty(filter))
        //    {
        //        logicSql += " " + filter;
        //    }
        //    else
        //    {
        //        filter = " 1=1";
        //        logicSql += "  1=1";
        //    }
        //    //分组
        //    if (!string.IsNullOrEmpty(group))
        //    {
        //        logicSql += " group by " + group;
        //    }

        //    #endregion

        //    //获取当前条件下数据总条数
        //    int count = _dbContext.Database.SqlQuery(typeof(int), "select count(*) from (" + tableName + ") where " + filter, para).Cast<int>().FirstOrDefault();
        //    string sql = "SELECT T.* FROM ( SELECT B.* FROM ( SELECT A.*,ROW_NUMBER() OVER(ORDER BY getdate()) as RN" +
        //                 logicSql + ") A ) B WHERE B.RN<=" + end + ") T WHERE T.RN>" + start;
        //    //排序
        //    if (!string.IsNullOrEmpty(orderby))
        //    {
        //        sql += " order by " + orderby;
        //    }
        //    var list = ExecuteSqlQuery(sql, para) as IEnumerable;
        //    if (list != null)
        //        return new PageInfo(index, pageSize, count, list.Cast<object>().ToList());
        //    return new PageInfo(index, pageSize, count, new { });
        //}

        ///// <summary>
        ///// 最简单的SQL分页
        ///// </summary>
        ///// <param name="index">页码</param>
        ///// <param name="pageSize">显示行数</param>
        ///// <param name="sql">纯SQL语句</param>
        ///// <param name="orderby">排序字段与方向</param>
        ///// <returns></returns>
        //public virtual PageInfo Query(int index, int pageSize, string sql, string orderby, params DbParameter[] para)
        //{
        //    return this.Query(index, pageSize, sql, null, null, orderby, null, para);
        //}
        /// <summary>
        /// 多表联合分页算法
        /// </summary>
        public virtual PageInfo Query(IQueryable query, int index, int PageSize)
        {
            var enumerable = (query as System.Collections.IEnumerable).Cast<object>();
            if (index < 1)
            {
                index = 1;
            }
            if (PageSize <= 0)
            {
                PageSize = 20;
            }

            int count = enumerable.Count();

            int maxpage = count / PageSize;

            if (count % PageSize > 0)
            {
                maxpage++;
            }
            if (index > maxpage)
            {
                index = maxpage;
            }
            if (count > 0)
                enumerable = enumerable.Skip((index - 1) * PageSize).Take(PageSize);
            return new PageInfo(index, PageSize, count, enumerable.ToList());
        }
        #endregion

        #region ADO.NET增删改查方法
        /// <summary>
        /// 执行增删改方法,含事务处理
        /// </summary>
        public virtual object ExecuteSqlCommand(string sql, params DbParameter[] para)
        {
            try
            {
                return _dbContext.Database.ExecuteSqlCommand(sql, para);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        ///// <summary>
        ///// 执行多条SQL，增删改方法,含事务处理
        ///// </summary>
        //public virtual object ExecuteSqlCommand(Dictionary<string, object> sqllist)
        //{
        //    try
        //    {
        //        int rows = 0;
        //        IEnumerator<KeyValuePair<string, object>> enumerator = sqllist.GetEnumerator();
        //        using (Transaction)
        //        {
        //            while (enumerator.MoveNext())
        //            {
        //                rows += _dbContext.Database.ExecuteSqlCommand(enumerator.Current.Key, enumerator.Current.Value);
        //            }
        //            Commit();
        //        }
        //        return rows;
        //    }
        //    catch (Exception e)
        //    {
        //        Rollback();
        //        throw e;
        //    }

        //}
        ///// <summary>
        ///// 执行查询方法,返回动态类，接收使用var，遍历时使用dynamic类型
        ///// </summary>
        //public virtual object ExecuteSqlQuery(string sql, params DbParameter[] para)
        //{
        //    try
        //    {
        //        return _dbContext.Database.SqlQueryForDynamic(sql, para);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        #endregion
    }
}
