<template>
  <em-panel v-bind="panel">
    <el-input placeholder="输入关键字进行过滤" v-model="filterText" size='mini'></el-input>
    <el-tree
      ref="tree"
      v-bind="tree"
      :filter-node-method="filterNode"
      @node-click="handleNodeClick"
    >
      <span slot-scope="{ data }">
        <em-icon name="role"   size="14px" />
        <span class="tree-label">{{ data.label }}</span>
      </span>
    </el-tree>
  </em-panel>
</template>
<script>
const { getTree } = $api.admin.role
export default {
  data() {
    return {
      filterText: '',
      panel: {
        header: true,
        title: '系统角色',
        icon: 'apartment',
        page: true
      },
      tree: {
        data: [],
        nodeKey: 'id',
        highlightCurrent: true,
        props: { children: 'children', label: 'label' },
        expandOnClickNode: false
      }
    }
  },
  watch: {
    filterText(val) {
      this.$refs.tree.filter(val)
    }
  },
  methods: {
    //刷新
    refresh() {
      getTree().then(data => {
        this.tree.data = data
      })
    },
    //点击节点
    handleNodeClick(data) {
      this.$emit('change', data)
    },
    //过滤节点
    filterNode(value, data) {
      if (!value) return true
      return data.label.indexOf(value) !== -1
    }
  },
  created() {
    this.refresh()
  }
}
</script>
