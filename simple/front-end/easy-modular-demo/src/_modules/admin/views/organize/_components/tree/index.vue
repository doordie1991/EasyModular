<template>
  <em-panel v-bind="panel">
    <el-input
      placeholder="输入关键字进行过滤"
      v-model="filterText"
      size="mini"
      style="margin-bottom: 8px"
    >
    </el-input>
    <el-tree
      ref="tree"
      v-bind="tree"
      v-on="on"
      :filter-node-method="filterNode"
    >
      <span slot-scope="{ data }">
        <em-icon :name="data.level === 0 ? 'home' : 'attachment'" size="14px" />
        <span class="tree-label">{{ data.label }}</span>
      </span>
    </el-tree>
  </em-panel>
</template>
<script>
const { getTree } = $api.admin.organize
import { mapState } from "vuex"
export default {
  data() {
    return {
      filterText: "",
      panel: {
        header: true,
        title: "组织架构",
        icon: "apartment",
        page: true
      },
      tree: {
        data: [],
        nodeKey: "id",
        highlightCurrent: true,
        props: { children: "children", label: "label" },
        currentNodeKey: this.$emptyGuid,
        expandOnClickNode: false,
        defaultExpandedKeys: [this.$emptyGuid],
        defaultExpandAll: true
      },
      on: {
        "current-change": this.onChange,
        "node-expand": this.onNodeExpand,
        "node-collapse": this.onNodeCollapse
      },
      //选中的节点
      selection: null
    }
  },
  computed: {
    ...mapState("app/user", { tenantName: (s) => s.tenantName })
  },
  methods: {
    /**
     * @description:刷新
     * @param {*}
     */
    refresh() {
      this.getData()
    },

    /**
     * @description: 获取取数据
     */
    async getData() {
      const data = await getTree()
      const root = {
        id: this.$emptyGuid,
        level: 0,
        label: "组织架构",
        item: {
          id: this.$emptyGuid,
          name: "组织架构"
        },
        children: data
      }
      this.tree.data = [root]
    },

    /**
     * @description: 当前选中节点变化时触发的事件
     * @param {*} data
     */
    onChange(data) {
      if (this.selection === data) return

      this.tree.currentNodeKey = data.id
      this.selection = data
      this.$emit("change", this.selection)
    },

    /**
     * @description: 节点被展开时触发的事件
     * @param {*} data
     */
    onNodeExpand(data) {
      //记录展开的节点
      this.tree.defaultExpandedKeys.push(data.id)
    },

    /**
     * @description: 节点被关闭时触发的事件
     * @param {*} data
     */
    onNodeCollapse(data) {
      //移除展开的节点
      this.$_.pull(this.tree.defaultExpandedKeys, data.id)
    },

    /**
     * @description: 过滤节点
     * @param {*} value
     * @param {*} data
     */
    filterNode(value, data) {
      if (!value) return true
      return data.label.indexOf(value) !== -1
    },

    /**
     * @description: 插入节点
     * @param {*}
     */
    insert(data) {
      //设置子节点
      if (!data.children) {
        data.children = []
      }
      const selection = this.selection || this.tree.data[0]
      let children = selection.children
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
     * @description: 移除节点
     * @param {*} id
     */
    remove(id) {
      const selection = this.selection || this.tree.data[0]
      let children = selection.children
      for (let i = 0; i < children.length; i++) {
        let child = children[i]
        if (id === child.id) {
          children.splice(i, 1)
          return child
        }
      }
    },

    /**
     * @description: 更新节点
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
    }
  },
  watch: {
    filterText(val) {
      this.$refs.tree.filter(val)
    }
  },
  created() {
    this.refresh()
  }
}
</script>
