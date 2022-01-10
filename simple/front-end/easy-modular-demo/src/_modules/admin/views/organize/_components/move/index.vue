<template>
  <em-dialog v-bind="dialog" :visible.sync="visible_" footer no-scrollbar>
    <organize-tree @change="onChange" :header="false"></organize-tree>
    <template v-slot:footer>
      <em-button type="success" text="确认" @click="onConfirm" />
      <em-button type="info" text="取消" @click="hide" />
    </template>
  </em-dialog>
</template>

<script>
import { mixins } from 'easy-modular-ui'
import OrganizeTree from '../tree'
const { move } = $api.admin.organize

export default {
  components: { OrganizeTree },
  mixins: [mixins.visible],
  props: { id: String },
  data() {
    return {
      dialog: {
        title: '组织移动',
        icon: 'app',
        width: '1000px',
        height: '600px',
        padding: 10
      },
      targetId: null
    }
  },
  methods: {
    /**
     * @description: 区域选中
     * @param {*}
     */
    onChange(row) {
      this.targetId = row.id
    },

    /**
     * @description: 确认
     * @param {*}
     */
    async onConfirm() {
      if (!this.targetId) {
        await this._error('请选择需要移动的位置')
        return
      }

      const data = { sourceId: this.id, targetId: this.targetId }
      const result = await move(data)
      await this._success('移动成功')
      this.$emit('success')
      this.hide()
    }
  }
}
</script>
