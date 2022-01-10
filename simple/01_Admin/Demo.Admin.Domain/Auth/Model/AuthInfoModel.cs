using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 认证信息
    /// </summary>
    public class AuthInfoModel
    {
        /// <summary>
        /// 账户标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 性别（1:男,0:女）
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 用户角色Id
        /// </summary>
        public string RoleIds { get; set; }

        /// <summary>
        /// 用户角色编码
        /// </summary>
        public string RoleCodes { get; set; }

        /// <summary>
        /// 用户角色名称
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<UserMenuItem> Menus { get; set; }

        /// <summary>
        /// 按钮编码列表
        /// </summary>
        public IList<PermissionEntity> Permissions { get; set; }

        /// <summary>
        /// 皮肤
        /// </summary>
        public SkinEntity Skin { get; set; }

        /// <summary>
        /// 详情信息(用于扩展登录对象信息)
        /// </summary>
        public object Details { get; set; }

    }

    /// <summary>
    /// 菜单
    /// </summary>
    public class UserMenuItem
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父节点编号
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 根节点编号
        /// </summary>
        public string RootId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public MenuType Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 路由参数
        /// </summary>
        public string RouteParams { get; set; }

        /// <summary>
        /// 路由参数
        /// </summary>
        public string RouteQuery { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 图标颜色
        /// </summary>
        public string IconColor { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 链接菜单打开方式
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Show { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<UserMenuItem> Children { get; set; }
    }
}
