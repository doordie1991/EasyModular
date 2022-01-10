<template>
  <em-container>
    <em-split v-model="split">
      <template v-slot:left>
        <tree-page ref="tree" @change="onTreeChange" />
      </template>
      <template v-slot:right>
        <list-page ref="list" :parent="parent" @save-success="onSaveSuccess" @remove-success="onRemoveSuccess" />
      </template>
    </em-split>
  </em-container>
</template>
<script>
import page from './page'
import treePage from '../_components/tree'
import listPage from '../_components/list'

export default {
  name: page.name,
  components: { treePage, listPage },
  data() {
    return {
      split: 0.2,
      parent: {}
    }
  },
  computed: {},
  methods: {
    refreshList() {
      this.$refs.list.refresh()
    },
    onTreeChange({ id, label, item }) {
      if (item) {
        this.parent.id = id
        this.parent.name = label
        this.refreshList()
      }
    },
    onSaveSuccess(model, data, isAdd) {
      const nodeData = { id: model.id, label: model.menuName, item: Object.assign({}, data) }
      if (isAdd) {
        this.$refs.tree.insert(nodeData)
      } else {
        this.$refs.tree.update(nodeData)
      }
      this.refreshList()
    },
    onRemoveSuccess(id) {
      this.$refs.tree.remove(id)
      this.refreshList()
    }
  }
}
</script>
