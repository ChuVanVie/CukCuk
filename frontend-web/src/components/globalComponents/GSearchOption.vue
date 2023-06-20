<template>
    <div class="align-center search-option" v-outside="() => this.isShowOption = false" @click="showSearchOption">{{
        searchIcon }}</div>
    <div class="context-menu context-menu-up" style="width: 150px; padding:unset; top: 24px;" v-if="isShowOption">
        <div class="context-menu-item" style="padding-left: 8px;" v-for="(value, index) in searchOption" :key="index"
            @click="choose(value, index)">
            <i class="fas fa-circle" :class="{ 'selected-option': iconActive[index] }"
                style="color: #00FF00; border: 1px solid #000; border-radius: 8px; visibility: hidden"></i>
            <span class="item-text pg-item" style="padding-left: 12px;">{{ value }}</span>
        </div>
    </div>
</template>
<script>
import resource from '@/resources/resource';
export default {
    name: "GSeachOption",
    props: {
        searchOption: {
            default: resource.searchOption
        }
    },
    created() {
        this.iconActive[0] = true;
    },
    data() {
        return {
            searchIcon: "*",
            isShowOption: false,
            iconActive: []
        }
    },
    methods: {
        showSearchOption() {
            this.isShowOption = !this.isShowOption;
        },
        choose(value, index) {
            this.searchIcon = value.charAt(0);
            this.$emit("searchByCondition", this.searchIcon,)
            this.iconActive = [];
            this.iconActive[index] = true;
            this.isShowOption = false;
        }
    },
    emits: ["searchByCondition"]
}
</script>
<style scoped>
.search-option {
    width: 22px;
    height: 22px;
    margin-left: 2px;
    border: 1px solid #bbb;
    background-color: #fcfcfc;
    cursor: pointer;
}

.search-option:hover {
    border-color: #eb7728;
    background-color: #e8f4fc;
}

.selected-option {
    visibility: visible !important;
}
</style>