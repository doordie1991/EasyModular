<template>
  <em-dialog v-bind="dialog" :visible.sync="visible_" footer no-scrollbar>
    <menu-tree @change="onChange" :header="false"></menu-tree>
    <template v-slot:footer>
      <em-button type="success" text="确认" @click="onConfirm" />
      <em-button type="info" text="取消" @click="hide" />
    </template>
  </em-dialog>
</template>

<script>
import { mixins } from 'easy-modular-ui'
import MenuTree from '../tree'
const { move } = $api.admin.menu

export default {
  components: { MenuTree },
  mixins: [mixins.visible],
  props: { id: String },
  data() {
    return {
      dialog: {
        title: '菜单移动',
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
     * @description: 菜单选中
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
        await this._error('请选择需要移动的菜单位置')
        return
      }

      const data = { sourceId: this.id, targetId: this.targetId }
      const result = await move(data)
      await this._success('移动成功')
      this.hide()
    }
  }
}
</script>
