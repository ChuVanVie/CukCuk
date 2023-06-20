<template>
    <div v-if="this.type == 'header'" class="m-droplist m-droplist-header" v-outside="() => this.isShowDropList = false">
        <div class="m-input-container" @click="btnShowDropList">
            <input type="text" disabled v-model="textValue">
            <div class="icon-container toggle align-center">
                <!-- <i class="fas fa-chevron-up"></i> -->
                <i class="fas fa-caret-down" style="color: #ffffff; cursor: pointer;"></i>
            </div>
        </div>
        <div class="context-menu context-menu-up" style="padding:unset" v-if="isShowDropList">
            <div class="context-menu-item" style="padding-left: 8px;" v-for="(result, index) in results" :key="index"
                @click="choose(result)">
                <span class="item-text pg-item">{{ result }}</span>
            </div>
        </div>
    </div>
    <div v-if="this.type == 'content'" class="select-input" :class="{ 'border-error': emptyError }"
        v-outside="() => this.isShowDropList = false">
        <input type="text" disabled :value="modelValue" @input="$emit('update:modelValue', $event.target.value)">
        <div class="align-center" @click="btnShowDropList" style="width: 22px; cursor: pointer;">
            <i class="fas fa-caret-down" style="color: #95a1b7;"></i>
        </div>
        <div class="context-menu context-menu-up" style="padding:unset; top: 24px;" v-if="isShowDropList">
            <div class="context-menu-item" style="padding-left: 8px;" v-for="(result, index) in results" :key="index"
                @click="choose(result)">
                <span class="item-text pg-item">{{ result }}</span>
            </div>
        </div>
    </div>
    <div v-if="this.type == 'x-content'" class="select-input" :class="{ 'border-error': emptyError }"
        v-outside="() => this.isShowDropList = false">
        <!-- <input type="text" v-model="textValue" style="width: 156px;"> -->
        <input type="text" disabled style="width: 156px;" :value="modelValue"
            @input="$emit('update:modelValue', $event.target.value)">
        <div class="align-center" @click="btnShowDropList" style="width: 22px; cursor: pointer;">
            <i class="fas fa-caret-down" style="color: #95a1b7;"></i>
        </div>
        <div class="icon-plus align-center" @click="btnClickShowFormAdd"
            style="width: 22px; margin-left: 4px; cursor: pointer;"></div>
        <div class="context-menu context-menu-up" style="padding:unset; top: 24px;" v-if="isShowDropList">
            <div class="context-menu-item" style="padding-left: 8px;" v-for="(result, index) in results" :key="index"
                @click="choose(result)">
                <span class="item-text pg-item">{{ result }}</span>
            </div>
        </div>
    </div>
    <div v-if="this.type == 'y-content'" class="select-input" :class="{ 'border-error': emptyError }"
        v-outside="() => this.isShowDropList = false">
        <!-- <input type="text" v-model="textValue" style="width: 156px;"> -->
        <input type="text" disabled style="width: 156px;" :value="modelValue"
            @input="$emit('update:modelValue', $event.target.value)">
        <div class="align-center" @click="btnShowDropList" style="width: 22px; cursor: pointer;">
            <i class="fas fa-caret-down" style="color: #95a1b7;"></i>
        </div>
        <div class="icon-plus align-center" @click="btnClickShowFormAdd"
            style="width: 22px; margin-left: 4px; cursor: pointer;"></div>
        <div class="context-menu context-menu-up" style="padding:unset; top: 24px;" v-if="isShowDropList">
            <div class="context-menu-item" style="padding-left: 8px;" v-for="(result, index) in results" :key="index"
                @click="choose(result)">
                <span class="item-text pg-item">{{ result.value }}</span>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: "GDropList",
    props: {
        items: {
            type: Array,
            required: false,
            default: () => [],
        },
        textInput: String,
        type: String,
        emptyError: Boolean,
        modelValue: {

        }
    },
    created() {
        this.textValue = this.textInput;
    },
    data() {
        return {
            textValue: "--Chuỗi nhà hàng--",
            isShowDropList: false,
            results: [],
        }
    },
    methods: {
        btnShowDropList() {
            this.isShowDropList = !this.isShowDropList;
            this.results = this.items;
        },

        choose(result) {
            this.$emit("setValueInput", result);
            this.isShowDropList = false;
        },
        btnClickShowFormAdd() {
            this.isShowDropList = false;
            this.$emit("showFormAdd");
        }
    },
    emits: ["update:modelValue", "setValueInput", "showFormAdd"]

}
</script>
<style scoped>
.icon-plus {
    background: url('@/assets/img/IconSprite.png') no-repeat 0 -48px;
    width: 15px;
    height: 15px;
}
</style>