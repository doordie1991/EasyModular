export default {
  title: '任务分组',
  name: 'scheduling_job_group',
  path: '/scheduling/job/group',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'scheduling_jobGroup_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'scheduling_jobGroup_edit_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'scheduling_jobGroup_delete_delete'
    }
    
  },
  component: () => import('./index')
}
