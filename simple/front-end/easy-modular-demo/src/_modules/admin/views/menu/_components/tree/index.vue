<template>
  <em-panel v-bind="panel" :header="header">
    <el-input placeholder="输入关键字进行过滤" v-model="filterText" size="mini" style="margin-bottom:8px"> </el-input>
    <el-tree ref="tree" v-bind="tree" v-on="on" :filter-node-method="filterNode" :show-checkbox="showCheckbox" :default-expand-all="defaultExpandAll" node-key="id">
      <span slot-scope="{ data }">
        <em-icon :name="data.level === 0 ? 'block' : 'attachment'" size="14px" />
        <span class="tree-label">{{ data.label }}</span>
      </span>
    </el-tree>
  </em-panel>
</template>
<script>
const { getTree } = $api.admin.menu
import { mapState } from 'vuex'
export default {
  data() {
    return {
      filterText: '',
      panel: {
        title: '菜单预览',
        icon: 'apartment',
        page: true
      },
      tree: {
        data: [],
        nodeKey: 'id',
        highlightCurrent: true,
        props: { children: 'children', label: 'label' },
        currentNodeKey: this.$emptyGuid,
        expandOnClickNode: false,
        defaultExpandedKeys: [this.$emptyGuid]
      },
      on: {
        'current-change': this.onCurrentChange,
        'node-expand': this.onNodeExpand,
        'node-collapse': this.onNodeCollapse,
        'check-change': this.onCheckChange,
        check: this.onCheck
      },
      selection: null
    }
  },
  props: {
    showCheckbox: Boolean,
    defaultExpandAll: Boolean,
    header: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    ...mapState('app/system', { title: s => s.config.title })
  },
  methods: {
    /**
     * @description: 刷新
     * @param {*} init
     */
    async refresh(init) {
      const data = await getTree()
      const root = {
        id: this.$emptyGuid,
        level: 0,
        label: this.title || '权限管理系统',
        item: {
          id: this.$emptyGuid,
          name: this.title,
          organizeFullName: '/'
        },
        children: data
      }
      this.tree.data = [root]
      if (init) {
        //初始化触发一次change事件
        this.onCurrentChange(root)
      } else {
        //刷新要保留当前点击节点
        this.$nextTick(() => {
          this.$refs.tree.setCurrentKey(this.tree.currentNodeKey)
        })
      }
    },

    /**
     * @description: 当前选中节点变化时触发的事件
     * @param {*} data
     * @param {*} node
     */
    onCurrentChange(data, node) {
      if (this.selection === data) return

      this.tree.currentNodeKey = data.id
      this.selection = data
      this.$emit('change', this.selection, node)
    },

    /**
     * @description: 节点选中状态发生变化时的回调(选中复选框时)
     * @param {*} data
     * @param {*} checkedObject
     */
    onCheck(data, checkedObject) {
      this.$emit('check', data, checkedObject)
    },

    /**
     * @description: 节点选中状态发生变化时的回调
     * @param {*} data
     * @param {*} checked
     */
    onCheckChange(data, checked) {
      this.$emit('check-change', data, checked)
    },

    /**
     * @description: 节点被展开时触发的事件
     * @param {*} data
     */
    onNodeExpand(data) {
      this.tree.defaultExpandedKeys.push(data.id)
    },

    /**
     * @description: 节点被关闭时触发的事件
     * @param {*} data
     */
    onNodeCollapse(data) {
      this.$_.pull(this.tree.defaultExpandedKeys, data.id)
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
     * @description: 节点插入
     * @param {*} data
     */
    insert(data) {
      //设置子节点
      if (!data.children) {
        data.children = []
      }
      let children = this.selection.children
      //如果不包含子节点，直接push，否则需要根据序号排序
      if (children.length < 1) {
        children.push(data)
        return
      }
      for (let i = 0; i < children.length; i++) {
        if (data.sort < children[i].item.sort) {
          children.splice(i, 0, data)
          break
        }

        //如果是最后一个，则附加到最后一个节点后面
        if (i === children.length - 1) {
          children.push(data)
          break
        }
      }
    },

    /**
     * @description: 节点删除
     * @param {*} id
     */
    remove(id) {
      let children = this.selection.children
      for (let i = 0; i < children.length; i++) {
        let child = children[i]
        if (id === child.id) {
          children.splice(i, 1)
          return child
        }
      }
    },

    /**
     * @description: 节点更新
     * @param {*} model
     */
    update(model) {
      //先判断是否展开,已展开的先删除
      let expanded = this.$refs.tree.getNode(model.id).expanded
      if (!expanded) {
        this.$_.pull(this.tree.defaultExpandedKeys, model.id)
      }
      //保存原来的子节点，同时先删除，再添加，这样可以保证排序
      model.children = this.remove(model.id).children
      this.insert(model)
      //若是展开状态要再次展开
      if (expanded) {
        this.tree.defaultExpandedKeys.push(model.id)
      }
    },

    /**
     * @description: 设置目前勾选的节点
     * @param {*} checkedKeys
     */
    setCheckedKeys(checkedKeys) {
      this.$nextTick(() => {
        if (this.showCheckbox) {
          this.$refs.tree.setCheckedKeys(checkedKeys)
        }
      })
    },

    /**
     * @description: 排序，重新刷新
     * @param {*}
     */
    sort() {
      this.refresh()
    }
  },
  watch: {
    filterText(val) {
      this.$refs.tree.filter(val)
    }
  },
  created() {
    this.refresh(true)
  }
}
</script>
