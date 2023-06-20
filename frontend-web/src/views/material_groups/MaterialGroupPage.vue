<template>
    <div id="material-group-container" style="height: 100%;">
        <div class="items-center" style="padding: 8px 0 16px 0;">
            <div class="content-title">Danh mục</div>
            <div class="align-center" style=" padding: 0 8px;">
                <i class="fas fa-caret-right" style="color: #0072bc; font-size: 18px;"></i>
            </div>
            <div class="sub-title">Nhóm nguyên vật liệu</div>
        </div>
        <div class="flex operations">
            <button class="m-btn m-btn-operate align-center" @click="showFormAddMaterialGroup">
                <div class="icon-add"></div>
                <p>Thêm</p>
            </button>
            <button class="m-btn m-btn-operate align-center">
                <div class="icon-update"></div>
                <p>Sửa</p>
            </button>
            <button class="m-btn m-btn-operate align-center">
                <div class="icon-delete"></div>
                <p>Xóa</p>
            </button>
            <button class="m-btn m-btn-operate align-center">
                <div class="icon-reload"></div>
                <p>Nạp</p>
            </button>
            <button class="m-btn m-btn-operate align-center">
                <div class="icon-expand"></div>
                <p>Mở rộng</p>
            </button>
            <button class="m-btn m-btn-operate align-center">
                <div class="icon-help"></div>
                <p>Giúp</p>
            </button>
        </div>
        <div class="m-table-container" style="overflow: hidden;">
            <GLoading v-if="isShowLoading"></GLoading>
            <table class="m-table m-table-line">
                <thead>
                    <tr>
                        <td>
                            <p>Mã nhóm</p>
                        </td>
                        <td>
                            <p>Tên nhóm</p>
                        </td>
                        <td>
                            <p>Diễn giải</p>
                        </td>
                        <td style="width: 125px;">
                            <p>Ngừng theo dõi</p>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr :class="{ 'row-active': isActiveRow[index] }" v-for="(materialgroup, index) in materialGroups"
                        :key="index" @click="setActive(index)" @dblclick="showFormUpdateMaterialGroup(materialgroup)">
                        <td>{{ materialgroup.materialGroupCode }}</td>
                        <td>{{ materialgroup.groupName }}</td>
                        <td>{{ materialgroup.explanation }}</td>
                        <td class="text-align--center"><input type="checkbox" name="" id=""></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <MaterialGroupForm v-if="isShowMaterialGroupForm" :materialgroupData="materialgroupSelected" :formDetail="formMode"
        @closeForm="this.isShowMaterialGroupForm = false">
    </MaterialGroupForm>
</template>
<script>
import GLoading from '@/components/globalComponents/GLoading.vue';
import MaterialGroupForm from '@/views/material_groups/MaterialGroupForm.vue';
import MaterialGroupService from '@/services/MaterialGroupService';
import Enum from '@/resources/enum';

const MaterialGroupAPI = new MaterialGroupService();

export default {
    name: "MaterialGroupPage",
    components: {
        GLoading, MaterialGroupForm
    },
    created() {
        this.getMaterialGroupsData();
    },
    props: {

    },
    data() {
        return {
            materialGroups: {},
            materialgroupSelected: null,
            formMode: null,
            isShowMaterialGroupForm: false,
            isActiveRow: [],
            isShowLoading: false,
        }
    },
    methods: {
        async getMaterialGroupsData() {
            this.isShowLoading = true;
            try {
                let res = await MaterialGroupAPI.getAll();
                this.materialGroups = res.data;
            }
            catch (error) {
                console.log(error);
            }
            finally {
                this.isShowLoading = false;
            }
        },

        showFormAddMaterialGroup() {
            this.formMode = Enum.FormMode.Add;
            this.isShowMaterialGroupForm = true;
        },

        showFormUpdateMaterialGroup(materialgroup) {
            this.formMode = Enum.FormMode.Update;
            this.materialgroupSelected = materialgroup;
            this.isShowMaterialGroupForm = true;
        },

        setActive(n) {
            this.isActiveRow = [];
            this.isActiveRow[n] = true;
        }
    }
}
</script>
<style scoped>
#material-container {
    color: #000;
    line-height: 17px;
}

.content-title {
    font-size: 28px;
}

.sub-title {
    color: #0072bc;
    font-size: 18px;
}

.operations {
    background-color: #f2f2f2 !important;
    border: 1px solid #bbb;
    border-bottom: unset !important;
    padding: 2px;
}

.row-active {
    background-color: #acd4ef;
}



.icon-add {
    background: url('../../assets/img/IconSprite.png') no-repeat -1px -2648px;
    width: 14px;
    height: 16px;
    margin-right: 4px;
}

.icon-copy {
    background: url('../../assets/img/IconSprite.png') no-repeat -1px -1888px;
    width: 14px;
    height: 15px;
    margin-right: 4px;
}

.icon-update {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1872px;
    width: 15px;
    height: 15px;
    margin-right: 4px;
}

.icon-delete {
    background: url('../../assets/img/IconSprite.png') no-repeat -2px -2683px;
    width: 12px;
    height: 11px;
    margin-right: 4px;
}

.icon-reload {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -2586px;
    width: 16px;
    height: 13px;
    margin-right: 4px;
}

.icon-expand {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1793px;
    width: 15px;
    height: 14px;
    margin-right: 4px;
}

.icon-help {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1600px;
    width: 16px;
    height: 16px;
    margin-right: 4px;
}
</style>