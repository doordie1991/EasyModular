<template>
  <em-dialog v-bind="dialog" :visible.sync="visible_" @opened="onOpened">
    <el-input class="query" placeholder="输入关键字进行过滤" v-model="keywords" :size="fontSize"> </el-input>
    <el-tree ref="tree" v-bind="tree" :render-content="renderContent" :filter-node-method="filterNode" @check-change="onMenuCheck"> </el-tree>

    <template v-slot:footer>
      <em-button type="success" text="确认" @click="onConfirm" />
      <em-button type="info" text="取消" @click="hide" />
    </template>
  </em-dialog>
</template>
<script>
import { mixins } from 'easy-modular-ui'

// 接口
const api = $api.admin.role

export default {
  mixins: [mixins.visible],
  data() {
    return {
      keywords: '',
      dialog: {
        title: '绑定菜单',
        icon: 'bind',
        width: '80%',
        height: '80%',
        padding: 10,
        footer: true
      },
      tree: {
        data: [],
        defaultCheckedKeys: [],
        highlightCurrent: true,
        showCheckbox: true,
        defaultExpandAll: true,
        nodeKey: 'id'
      },
      treeData: [],
      roleMenuIds: []
    }
  },
  props: {
    id: {
      type: String,
      required: true
    }
  },
  watch: {
    keywords(val) {
      this.$refs.tree.filter(val)
    }
  },
  methods: {
    /**
     * @description: 窗口打开事件
     * @param {*}
     */
    onOpened() {
      this.getData()
    },

    /**
     * @description:获取数据
     * @param {*}
     */
    async getData() {
      const params = { id: this.id }
      const result = await api.menuTree(params)
      this.tree.data = result.menus
      this.tree.defaultCheckedKeys = result.roleMenuIds
    },

    /**
     * @description:确认
     * @param {*}
     */
    async onConfirm() {
      let menus = []
      const checkNodes = this.$refs.tree.getCheckedNodes(false, false)
      checkNodes.forEach(m => {
        m.item.menuChecked = true
        menus.push(m.item)
      })

      const halfCheckNodes = this.$refs.tree.getHalfCheckedNodes()
      halfCheckNodes.forEach(m => {
        m.item.menuChecked = false
        menus.push(m.item)
      })

      const params = { roleId: this.id, menus: menus }
      await api.bindMenuPermission(params)
      await this._success('绑定成功')
      await this.hide()
    },

    /**
     * @description:渲染树
     * @param {*} h
     * @param {*} node
     * @param {*} data
     */
    renderContent(h, { node, data }) {
      return (
        <span class="custom-tree-node">
          <span>{node.label}</span>
          <span>
            {data.item.buttons.map(m => {
              return (
                <el-checkbox v-model={m.checked} disabled={m.disabled}>
                  {m.buttonName}
                </el-checkbox>
              )
            })}
          </span>
        </span>
      )
    },

    /**
     * @description: 节点过滤
     * @param {*} value
     * @param {*} data
     */
    filterNode(value, data) {
      if (!value) return true
      return data.label.indexOf(value) !== -1
    },

    /**
     * @description: 菜单选中
     * @param {*} node
     * @param {*} state
     */
    onMenuCheck(node, state, child) {
      node.item.buttons.forEach(m => {
        m.disabled = !state
      })
    }
  }
}
</script>

<style>
.custom-tree-node {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 14px;
  padding-right: 8px;
}
.query {
  margin: 12px auto;
}
</style>
