using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.Utils
{
    /// <summary>
    /// lambda表达式 辅助类
    /// </summary>
    public class ExpressionHelper
    {
        /// <summary>
        /// 创建lambda表达式：p=>true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            return p => true;
        }

        /// <summary>
        /// 创建lambda表达式：p=>false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            return p => false;
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        public static Expression<Func<T, TKey>> GetPropertyExpression<T, TKey>(string propertyName,string parameterName = "x")
        {
            var parameter = Expression.Parameter(typeof(T), parameterName);
            var property = Expression.Property(parameter, propertyName);

            return Expression.Lambda<Func<T, TKey>>(property, parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName == propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateEqual<T>(string propertyName, object propertyValue, string parameterName = "x")
        {
            var parameter = Expression.Parameter(typeof(T), parameterName);
            var member = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Constant(propertyValue);

            return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName != propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateNotEqual<T>(string propertyName, object propertyValue, string parameterName = "x")
        {
            var parameter = Expression.Parameter(typeof(T), parameterName);
            var member = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Constant(propertyValue);

            return Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName > propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateGreaterThan<T>(string propertyName, object propertyValue, string parameterName = "x")
        {
            var parameter = Expression.Parameter(typeof(T), parameterName);
            var member = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Constant(propertyValue);

            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName < propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateLessThan<T>(string propertyName, object propertyValue, string parameterName = "x")
        {
            var parameter = Expression.Parameter(typeof(T), parameterName);
            var member = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Constant(propertyValue);

            return Expression.Lambda<Func<T, bool>>(Expression.LessThan(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName >= propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateGreaterThanOrEqual<T>(string propertyName, object propertyValue, string parameterName = "x")
        {
            var parameter = Expression.Parameter(typeof(T), parameterName);
            var member = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Constant(propertyValue);

            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName <= propertyValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateLessThanOrEqual<T>(string propertyName, object propertyValue, string parameterName = "x")
        {
            var parameter = Expression.Parameter(typeof(T), parameterName);
            var member = Expression.PropertyOrField(parameter, propertyName);
            var constant = Expression.Constant(propertyValue);

            return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(member, constant), parameter);
        }

        /// <summary>
        ///  创建lambda表达式：p=>p.propertyName.Contains(propertyValue)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> GetContains<T>(string propertyName, object propertyValue, string parameterName = "x")
        {
            var parameter = Expression.Parameter(typeof(T), parameterName);
            var member = Expression.PropertyOrField(parameter, propertyName);
            var method = typeof(string).GetMethod("Contains", new[] { typeof(object) });
            var constant = Expression.Constant(propertyValue, typeof(object));

            return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameter);
        }

        /// <summary>
        /// 创建lambda表达式：!(p=>p.propertyName.Contains(propertyValue))
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> GetNotContains<T>(string propertyName, object propertyValue, string parameterName = "x")
        {
            var parameter = Expression.Parameter(typeof(T), parameterName);
            var member = Expression.PropertyOrField(parameter, propertyName);
            var method = typeof(string).GetMethod("Contains", new[] { typeof(object) });
            var constant = Expression.Constant(propertyValue, typeof(object));

            return Expression.Lambda<Func<T, bool>>(Expression.Not(Expression.Call(member, method, constant)), parameter);
        }


        /// <summary>
        /// 创建lambda表达式：p=>data.Contains(p.code)
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        public static Expression<Func<T1, bool>> GetIn<T1, T2>(string propertyName, IQueryable<T2> propertyValue, string parameterName = "x")
        {
            var parameter = Expression.Parameter(typeof(T1), parameterName);
            var member = Expression.Property(parameter, propertyName);
            var method = typeof(Queryable).GetMethods().Single(m => m.Name == nameof(Queryable.Contains) && m.GetParameters().Length == 2).MakeGenericMethod(typeof(T2));

            return Expression.Lambda<Func<T1, bool>>(Expression.Call(null, method, new Expression[] { Expression.Constant(propertyValue), member }), parameter);
        }

        /// <summary>
        ///  创建lambda表达式：p=>!data.Contains(p.code)
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        public static Expression<Func<T1, bool>> GetNotIn<T1, T2>(string propertyName, IQueryable<T2> propertyValue,string parameterName = "x")
        {
            var parameter = Expression.Parameter(typeof(T1), parameterName);
            var member = Expression.Property(parameter, propertyName);
            var method = typeof(Queryable).GetMethods().Single(m => m.Name == nameof(Queryable.Contains) && m.GetParameters().Length == 2).MakeGenericMethod(typeof(T2));

            return Expression.Lambda<Func<T1, bool>>(Expression.Not(Expression.Call(null, method, new Expression[] { Expression.Constant(propertyValue), member })), parameter);
        }
    }
}
