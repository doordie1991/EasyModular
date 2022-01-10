export default {
  title: '导入模板',
  name: 'admin_import_template',
  path: '/admin/import/template',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'admin_importTemplate_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'admin_importTemplate_edit_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'admin_importTemplate_delete_delete'
    }
  },
  component: () => import('./index')
}
