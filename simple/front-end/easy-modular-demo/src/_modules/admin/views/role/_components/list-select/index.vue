<template>
  <em-dialog v-bind="dialog" :visible.sync="visible_" no-scrollbar @opened="onOpen">
    <em-list ref="list" v-bind="list" @select="select" @select-all="select">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
      </template>
    </em-list>
    <template v-slot:footer>
      <em-button type="success" size="small" @click="confirm">确定</em-button>
      <em-button type="info" size="small" @click="visible_ = false">关闭</em-button>
    </template>
  </em-dialog>
</template>

<script>
import { mixins } from 'easy-modular-ui'
import cols from '../../index/cols'

const api = $api.admin.role

export default {
  mixins: [mixins.list, mixins.visible],
  data() {
    return {
      selection_: [],
      dialog: {
        title: '选择角色',
        header: true,
        footer: true,
        icon: 'app',
        width: '70%',
        height: '75%',
        visible: false
      },

      list: {
        cols,
        action: api.query,
        multiple: true,
        noHeader: true,
        noOperateColumn: true,
        queryOnCreated: false,
        model: {
          //角色名称
          name: '',
          //排序
          orderFileds: 'createdTime desc'
        }
      }
    }
  },
  methods: {
    //选择
    select(selection) {
      this.selection_ = selection
    },
    //数据响应
    confirm() {
      this.hide()
      this.$emit('change', this.selection_)
    },
    //窗口打开后事件
    onOpen() {
      this.refresh()
    }
  }
}
</script>
