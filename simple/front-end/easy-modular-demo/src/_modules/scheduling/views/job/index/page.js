export default {
  title: '任务管理',
  name: 'scheduling_job',
  path: '/scheduling/job/index',
  buttons: {
    add: {
      text: '添加',
      type: 'success',
      icon: 'plus-circle',
      code: 'scheduling_job_add_post'
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: 'scheduling_job_edit_get'
    },
    pause: {
      text: '暂停',
      type: 'text',
      icon: 'pause',
      code:'scheduling_job_pause_post',
    },
    resume: {
      text: '启动',
      type: 'text',
      icon: 'run',
      code:'scheduling_job_resume_post',
    },
    stop: {
      text: '停止',
      type: 'text',
      icon: 'stop',
      code:'scheduling_job_stop_post',
    },
    log: {
      text: '日志',
      type: 'text',
      icon: 'search',
      code:'scheduling_jobLog_query_get'
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: 'scheduling_job_delete_delete'
    }
    
  },
  component: () => import('./index')
}
