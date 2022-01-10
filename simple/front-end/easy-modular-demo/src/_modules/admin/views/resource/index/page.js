export default {
  title: '资源管理',
  name: 'admin_resource',
  path: '/admin/resource/index',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_resource_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_resource_edit_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_resource_delete_delete'
    },
    permissions: {
      text: '权限设置',
      type: 'text',
      icon: 'block',
      code: 'admin_resource_permissions_post'
    },
    sync: {
      text: '同步',
      type: 'danger',
      icon: 'database',
      code: 'admin_resource_sync_get'
    }
  },
  component: () => import('./index')
}
