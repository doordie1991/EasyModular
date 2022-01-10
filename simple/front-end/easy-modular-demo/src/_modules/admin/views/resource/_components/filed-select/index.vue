<template>
  <div class="resource-detail-select">
    <em-select v-model="value_" :action="action" @change="onChange"></em-select>
  </div>
</template>

<script>
import { mixins } from "easy-modular-ui"
const api = $api.admin.resourceDetail

export default {
  mixins: [mixins.select],
  data() {
    return {
      action: this.query
    }
  },
  props: {
    resourceId: {
      type: String,
      required: true
    }
  },
  methods: {
    query() {
      return api.select({ resourceId: this.resourceId })
    }
  }
}
</script>
