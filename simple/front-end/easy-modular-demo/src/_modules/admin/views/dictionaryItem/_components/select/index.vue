<template>
  <div class="dictionary-select">
    <em-select v-model="value_" :action="action" @change="onChange"></em-select>
  </div>
</template>

<script>
import { mixins } from 'easy-modular-ui'
const api = $api.admin.dictionary

export default {
  mixins: [mixins.select],
  data() {
    return {
      action: this.query
    }
  },
  props: {
    group: {
      type: String,
      required: true
    },
    code: {
      type: String,
      required: true
    }
  },
  methods: {
    query() {
      return api.select({ group: this.group, code: this.code })
    }
  }
}
</script>
