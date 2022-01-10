<template>
  <div class="dictionary-group-select">
    <em-select v-model="value_" :action="action" @change="onChange"></em-select>
  </div>
</template>

<script>
import { mixins } from "easy-modular-ui"
const api = $api.admin.dictionaryGroup

export default {
  mixins: [mixins.select],
  data() {
    return {
      action: api.select
    }
  }
}
</script>
