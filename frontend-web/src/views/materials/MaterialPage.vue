<template>
    <div id="material-container">
        <div class="items-center" style="padding: 8px 0 16px 0;">
            <div class="content-title">Danh mục</div>
            <div class="align-center" style=" padding: 0 8px;">
                <i class="fas fa-caret-right" style="color: #0072bc; font-size: 18px;"></i>
            </div>
            <div class="sub-title">Nguyên vật liệu..</div>
        </div>
        <div class="flex operations">
            <button class="m-btn m-btn-operate align-center" @click="btnShowFormAddMaterial">
                <div class="icon-add"></div>
                <p>Thêm</p>
            </button>
            <button class="m-btn m-btn-operate align-center" @click="btnOpenFormUpdateMaterial">
                <div class="icon-update"></div>
                <p>Sửa</p>
            </button>
            <button class="m-btn m-btn-operate align-center" @click="btnDeleteMaterial">
                <div class="icon-delete"></div>
                <p>Xóa</p>
            </button>
            <button class="m-btn m-btn-operate align-center" @click="reloadPage">
                <div class="icon-reload"></div>
                <p>Nạp</p>
            </button>
            <!-- <button class="m-btn m-btn-operate align-center" @click="this.isShowMaterialFormImport = true">
                <div class="icon-import"></div>
                <p>Nhập khẩu</p>
            </button> -->
            <button class="m-btn m-btn-operate align-center" @click="exportToExcelFile">
                <div class="icon-export"></div>
                <p>Xuất khẩu</p>
            </button>
        </div>
        <div class="m-table-container">
            <GLoading v-if="isShowLoading"></GLoading>
            <table class="m-table m-table-line">
                <thead>
                    <tr>
                        <td>
                            <p>Mã nguyên vật liệu</p>
                            <div class="search-option-container items-center">
                                <!-- <div class="align-center search-option">*</div> -->
                                <GSearchOption @searchByCondition="searchConditionCode"></GSearchOption>
                                <input type="text" class="m-input" v-model="stringCode" @keydown.enter="search">
                            </div>
                        </td>
                        <td>
                            <p>Tên nguyên vật liệu</p>
                            <div class="search-option-container items-center">
                                <GSearchOption @searchByCondition="searchConditionName"></GSearchOption>
                                <input type="text" class="m-input" v-model="stringName" @keydown.enter="search">
                            </div>
                        </td>
                        <td>
                            <p>Tính chất</p>
                            <GDropList type="content" :items="properties" v-model="stringProperty"
                                @setValueInput="searchByProperty"></GDropList>
                        </td>
                        <td>
                            <p>ĐVT chính</p>
                            <div class="search-option-container items-center">
                                <GSearchOption @searchByCondition="searchConditionUnit"></GSearchOption>
                                <input type="text" class="m-input" v-model="stringUnit" @keydown.enter="search">
                            </div>
                        </td>
                        <td>
                            <p>Nhóm nguyên vật liệu</p>
                            <div class="search-option-container items-center">
                                <GSearchOption @searchByCondition="searchConditionGroup">
                                </GSearchOption>
                                <input type="text" class="m-input" v-model="stringMaterialGroup" @keydown.enter="search">
                            </div>
                        </td>
                        <td>
                            <p>Ghi chú</p>
                            <div class="search-option-container items-center">
                                <GSearchOption @searchByCondition="searchConditionNote"></GSearchOption>
                                <input type="text" class="m-input" v-model="stringNote" @keydown.enter="search">
                            </div>
                        </td>
                        <td style="width: 125px">
                            <p>Ngừng theo dõi</p>
                            <div class="select-input"
                                v-outside="() => this.stopfollowDropList.isShowStopFollowDropList = false">
                                <input type="text" style="width: 96px; background-color: #fff;" disabled
                                    v-model="stopfollowDropList.stopfollowValue">
                                <div class="align-center" @click="showStopFollowDropList"
                                    style="width: 22px; cursor: pointer;">
                                    <i class="fas fa-caret-down" style="color: #95a1b7;"></i>
                                </div>
                                <div class="context-menu context-menu-up" style="padding:unset; top: 24px;"
                                    v-if="stopfollowDropList.isShowStopFollowDropList">
                                    <div class="context-menu-item" style="padding-left: 4px;"
                                        v-for="(value, index) in stopfollows" :key="index" @click="chooseStopFollow(value)">
                                        <span class="item-text pg-item">{{ value }}</span>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr :class="{ 'row-active': isActiveRow[index] }" v-for="(material, index) in materials" :key="index"
                        @click="setRowActive(material, index)" @dblclick="showFormUpdateMaterial(material)">
                        <td>{{ material.materialCode }}</td>
                        <td>{{ material.materialName }}</td>
                        <td>{{ material.materialProperty }}</td>
                        <td>{{ material.calculationUnitName }}</td>
                        <td>{{ material.groupName }}</td>
                        <td>{{ material.note }}</td>
                        <td @dblclick.stop class="text-align--center"><input type="checkbox" disabled name="" id=""
                                v-model="valueStopFollow"></td>
                    </tr>
                </tbody>
            </table>
            <div v-if="!this.materials[0]" class="notFoundMaterial"> Không tìm thấy thông tin nguyên vật liệu.
            </div>
        </div>
        <div class="pagination">
            <div class="left-pagination items-center">
                <div class="icon-first-page tooltip" @click="btnFirstPage">
                    <span class="tooltiptext">Trang đầu</span>
                </div>
                <div class="icon-prev-page tooltip" @click="btnPrevPage">
                    <span class="tooltiptext">Trang trước</span>
                </div>
                <div style="border: 1px solid #bbb; height: 16px; margin-right: 16px"></div>
                <div class="page-number">
                    Trang <input type="text" class="m-input" v-model="pageNumber" @keydown.enter="enterPageNumber"> trên {{
                        totalPages }}
                </div>
                <div style="border: 1px solid #bbb; height: 16px; margin: 0 16px"></div>
                <div class="icon-next-page tooltip" @click="btnNextPage">
                    <span class="tooltiptext">Trang sau</span>
                </div>
                <div class="icon-last-page tooltip" @click="btnLastPage">
                    <span class="tooltiptext">Trang cuối</span>
                </div>
                <div class="icon-load-page items-center tooltip" @click="reloadPage">
                    <i class="fas fa-redo"></i>
                    <span class="tooltiptext">Tải lại</span>
                </div>
                <div class="numberRecords-container" v-outside="() => this.isShowRecordDropList = false">
                    <input type="text" style="width: 100%; padding-left: 4px; border: unset;" disabled v-model="pageSize">
                    <div class="align-center" @click="showRecordDropList" style="width: 30px; cursor: pointer;">
                        <i class="fas fa-caret-up" style="color: #95a1b7;"></i>
                    </div>
                    <div class="context-menu context-menu-up" style="padding:unset; top: -80px;"
                        v-if="isShowRecordDropList">
                        <div class="context-menu-item" style="padding-left: 4px;" v-for="(number, index) in numberrecords"
                            :key="index" @click="chooseNumberRecords(number)">
                            <span class="item-text pg-item">{{ number }}</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="right-pagination items-center" style="margin-right: 12px;">
                Hiển thị {{ startRecord }}-{{ endRecord }} trên {{ totalRecords }} kết quả
            </div>
        </div>
    </div>
    <MaterialForm v-if="isShowMaterialForm" ref="materialform" :materialData="materialSelected" :formDetail="formMode"
        @closeForm="this.isShowMaterialForm = false" @openNewForm="this.isShowMaterialForm = true" @reload="reloadPage"
        @onShowToast="showSuccesToast">
    </MaterialForm>
    <MaterialFormImport v-if="isShowMaterialFormImport" @closeForm="this.isShowMaterialFormImport = false">
    </MaterialFormImport>
    <GDialog v-if="gdialog.isShowDialog" :type="gdialog.typeDialog" :materialCode="gdialog.materialCodeDelete"
        @clickAgree="btnAgreeDelete" @clickCancel="btnCancelDelete"></GDialog>
    <GDialogErrorApi v-if="isShowDialogErrorApi" @clickAgree="this.isShowDialogErrorApi = false"></GDialogErrorApi>
    <GToast v-if="isShowToast" :msgToast="msgToast"></GToast>
</template>
<script>
import GLoading from '@/components/globalComponents/GLoading.vue';
import GSearchOption from '@/components/globalComponents/GSearchOption.vue';
import GDropList from '@/components/globalComponents/GDroplist.vue';
import GDialog from '@/components/globalComponents/GDialog.vue';
import GDialogErrorApi from '@/components/globalComponents/GDialogErrorAPI.vue';
import GToast from '@/components/globalComponents/GToast.vue';
import MaterialForm from './MaterialForm.vue';
import MaterialFormImport from './MaterialFormImport.vue';
import resource from '@/resources/resource';
import Enum from '@/resources/enum';
const XLSX = require('sheetjs-style');
require('xlsx');
import MaterialService from '@/services/MaterialService';

const MaterialAPI = new MaterialService();

export default {
    name: "MaterialPage",
    components: {
        GLoading, GSearchOption, GDropList, GDialog, GDialogErrorApi, GToast, MaterialForm, MaterialFormImport
    },
    created() {
        this.getAllMaterials();
        this.getMaterialsData();
    },
    props: {
        properties: {
            default: resource.materials.properties
        },
        stopfollows: {
            default: resource.stopFollow
        },
        numberrecords: {
            default: resource.numberRecords
        }
    },
    data() {
        return {
            materials: {},
            allMaterials: {},
            materialSelected: null,
            //chuỗi tìm kiếm theo trường
            stringCode: null,
            iconCode: "*",
            stringName: null,
            iconName: "*",
            stringProperty: null,
            stringUnit: null,
            iconUnit: "*",
            stringMaterialGroup: null,
            iconGroup: "*",
            stringNote: null,
            iconNote: "*",
            stringStopFollow: null,
            valueStopFollow: null,
            formMode: null, //trạng thái mở form (thêm hoặc sửa)
            isActiveRow: [], //Mảng lưu dòng đang click
            isShowLoading: false,
            isShowDialogErrorApi: false,
            isShowToast: false,
            msgToast: null,
            isShowRecordDropList: false,
            pageNumber: 1,
            pageSize: 25,
            totalRecords: null,
            totalPages: null,
            startRecord: null,
            endRecord: null,
            isShowMaterialForm: false,
            isShowMaterialFormImport: false,
            propertyDropList: {
                isShowPropertyDropList: false,
                propertyValue: null
            },
            stopfollowDropList: {
                isShowStopFollowDropList: false,
                stopfollowValue: null
            },
            gdialog: {
                isShowDialog: false,
                typeDialog: null,
                materialCodeDelete: null
            }
        }
    },
    methods: {

        /**
         * Hàm lấy tất cả bản ghi material
         * params: ()
         * Author: CVViệt
         * Date: 30/05/2023
         */
        async getAllMaterials() {
            try {
                let res = await MaterialAPI.getAll();
                this.allMaterials = res.data;
            } catch (error) {
                this.isShowDialogErrorApi = true;
            }
        },

        /**
         * Hàm lấy các bản ghi material theo phân trang, tìm kiếm 
         * params: ()
         * Author: CVViệt
         * Date: 30/05/2023
         */
        async getMaterialsData(stringCode = null, stringName = null, stringProperty = null, stringUnit = null, stringMaterialGroup = null, stringNote = null, stringStopFollow = null) {
            this.isShowLoading = true;
            try {
                let res = await MaterialAPI.getPaging(this.pageSize, this.pageNumber, stringCode, stringName, stringProperty, stringUnit, stringMaterialGroup, stringNote, stringStopFollow);
                // console.log(res);
                this.materials = res.data.listData;
                //Tổng số bản ghi thỏa mãn
                this.totalRecords = res.data.totalRecord;
                this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
                if (this.totalRecords == null || this.totalRecords == 0) {
                    this.pageNumber = 0;
                    this.startRecord = 0;
                    this.endRecord = 0;
                }
                else {
                    if (this.totalRecords < (this.pageNumber * this.pageSize)) {
                        this.startRecord = (this.pageNumber - 1) * this.pageSize + 1;
                        this.endRecord = this.totalRecords;
                    }
                    else {
                        this.startRecord = (this.pageNumber - 1) * this.pageSize + 1;
                        this.endRecord = this.pageNumber * this.pageSize;
                    }
                }
            }
            catch (error) {
                this.isShowDialogErrorApi = true;
            }
            finally {
                this.isShowLoading = false;
            }
        },

        /**
         * Hàm xóa 1 material 
         * params: materialId: id của nguyên vật liệu cần xóa
         * Author: CVViệt
         * Date: 30/05/2023
         */
        async deleteMaterial(materialId) {
            try {
                await MaterialAPI.delete(materialId);
            }
            catch (error) {
                this.isShowDialogErrorApi = true;
            }
        },


        /**
         * Hàm reload lại trang 
         * params: materialId: id của nguyên vật liệu cần xóa
         * Author: CVViệt
         * Date: 30/05/2023
         */
        reloadPage() {
            try {
                this.pageNumber = 1;
                this.pageSize = 25;
                this.isActiveRow = [];
                this.materialSelected = null;
                this.stringCode = null;
                this.stringName = null;
                this.stringProperty = null;
                this.stringUnit = null;
                this.stringMaterialGroup = null;
                this.stringNote = null;
                this.stringStopFollow = null;
                this.valueStopFollow = null;
                this.stopfollowDropList.stopfollowValue = null
                this.getMaterialsData();
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm xuất file excel từ thông tin nhân viên ở bảng
         * params: ()
         * Author: CVViệt ()
         * Date: 24/04/2023
         * Returns: 1 file excel tên 'Thông tin nhân viên.xlsx'
         */
        exportToExcelFile() {
            try {
                //Mảng lưu các giá trị là từng hàng trong file excel (dưới dạng mảng)
                const data = [];

                const title = resource.excelFile.title;
                const subtitle = [];
                data.push(title);
                data.push(subtitle);

                //Mảng các giá trị header của bảng
                const headers = resource.excelFile.header;
                data.push(headers);

                for (let i = 0; i < this.allMaterials.length; i++) {
                    //Mảng các giá trị tương ứng với header của bảng
                    let values = [];
                    values.push(i + 1);
                    values.push(this.allMaterials[i].materialCode);
                    values.push(this.allMaterials[i].materialName);
                    values.push(this.allMaterials[i].materialProperty);
                    values.push(this.allMaterials[i].calculationUnitName);
                    values.push(this.allMaterials[i].groupName);
                    values.push(this.allMaterials[i].note);
                    //Thêm vào mảng chứa data export
                    data.push(values);
                }

                //Hàm thư viện
                // Tạo đối tượng worksheet
                const ws = XLSX.utils.aoa_to_sheet(data, { header: headers, skipHeader: false });

                //Merge và format dòng title
                ws['!merges'] = [{ s: { r: 0, c: 0 }, e: { r: 0, c: headers.length - 1 } }, { s: { r: 1, c: 0 }, e: { r: 1, c: headers.length - 1 } }];

                //Format chiều rộng của các ô
                var wscols = [];
                // đặt chiều rộng của các cột dựa trên độ dài của headers
                headers.forEach((header) => {
                    wscols.push({ wch: header.length });
                });

                // đặt chiều rộng của các cột khác đủ rộng để hiển thị nội dung ô
                for (let i = 1; i < data.length; i++) {
                    const row = data[i];
                    for (let j = 1; j < headers.length; j++) {
                        const cell = row[j];
                        if (cell && cell.length > wscols[j].wch) {
                            wscols[j].wch = cell.length + 6;
                        }
                    }
                }
                ws["!cols"] = wscols;

                // Set the border for the range
                // const range = XLSX.utils.decode_range(ws['!ref']);
                // ws['!range'] = {
                //     ...range, s: {
                //         border: {
                //             top: { style: 'thin', color: { rgb: '000000' } },
                //             right: { style: 'thin', color: { rgb: '000000' } },
                //             bottom: { style: 'thin', color: { rgb: '000000' } },
                //             left: { style: 'thin', color: { rgb: '000000' } }
                //         }
                //     }
                // };

                // Format dòng title của worksheet
                const cellTitle = ws['A1'];
                cellTitle.s = {
                    font: { sz: 18, bold: true },
                    alignment: { vertical: 'middle', horizontal: 'center' },
                }

                // Format dòng header của worksheet
                const headerRows = XLSX.utils.sheet_to_json(ws, { header: 1 })[2];
                // Định dạng chữ đậm cho các cell trong dòng header
                headerRows.forEach((headerRow, index) => {
                    // Lấy tọa độ của cell
                    const cellAddress = XLSX.utils.encode_cell({ r: 2, c: index });
                    // Truy cập đến đối tượng CellObject của cell đó
                    const cell = XLSX.utils.sheet_get_cell(ws, cellAddress);
                    // Đặt thuộc tính s (style) của CellObject để format chữ đậm
                    cell.s = {
                        font: { bold: true },
                        alignment: { vertical: 'middle', horizontal: 'center' },
                        fill: { type: 'pattern', patternType: 'solid', fgColor: { rgb: 'DDDDDD' } },
                        border: {
                            top: { style: 'thin', color: { rgb: '000000' } },
                            right: { style: 'thin', color: { rgb: '000000' } },
                            bottom: { style: 'thin', color: { rgb: '000000' } },
                            left: { style: 'thin', color: { rgb: '000000' } }
                        }
                    };
                });

                // Tạo workbook và lưu file Excel
                const wb = XLSX.utils.book_new();
                XLSX.utils.book_append_sheet(wb, ws, 'Thông tin nguyên vật liệu');
                XLSX.writeFile(wb, 'Thông tin nguyên vật liệu.xlsx');
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm set background-color cho hàng đc click
         * params: bản ghi đang click
         * Author: CVViệt ()
         * Date: 27/05/2023
         */
        setRowActive(material, index) {
            try {
                this.isActiveRow = [];
                this.isActiveRow[index] = true;
                this.materialSelected = material;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm click btn thêm nguyên vật liệu mới
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        btnShowFormAddMaterial() {
            try {
                this.formMode = Enum.FormMode.Add;
                this.isShowMaterialForm = true;
            }
            catch (error) {
                console.log(error);
            }
        },


        /**
         * Hàm click btn sửa nguyeen vật liệu
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        btnOpenFormUpdateMaterial() {
            try {
                if (this.materialSelected != null) {
                    this.formMode = Enum.FormMode.Update;
                    this.isShowMaterialForm = true;
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm doubleclick hiện form sửa nguyên vật liệu
         * params: nguyên vật liệu cần sửa
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        showFormUpdateMaterial(material) {
            try {
                this.formMode = Enum.FormMode.Update;
                this.materialSelected = material;
                // console.log(this.materialSelected);
                this.isShowMaterialForm = true;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm click btn xóa NVL
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        btnDeleteMaterial() {
            try {
                if (this.materialSelected != null) {
                    this.formMode = Enum.FormMode.delete;
                    this.gdialog.typeDialog = 'delete';
                    this.gdialog.materialCodeDelete = this.materialSelected.materialCode;
                    this.gdialog.isShowDialog = true;
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm đồng ý xóa 1 bản ghi NVL
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        async btnAgreeDelete() {
            try {
                await MaterialAPI.delete(this.materialSelected.materialId);
                this.gdialog.isShowDialog = false;
                this.materialSelected = null;
                this.showSuccesToast();
                this.reloadPage();
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm hủy xóa 1 bản ghi NVL
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        btnCancelDelete() {
            this.gdialog.isShowDialog = false;
        },

        showPropertyDropList() {
            this.propertyDropList.isShowPropertyDropList = !this.propertyDropList.isShowPropertyDropList;
        },
        chooseProperty(property) {
            this.propertyDropList.propertyValue = property;
            this.propertyDropList.isShowPropertyDropList = false;
        },


        /**
         * Hàm hiện toast thông báo sự kiện thành công
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        showSuccesToast() {
            this.isShowToast = true;
            if (this.formMode == Enum.FormMode.Add) {
                this.msgToast = "Thêm mới nguyên vật liệu";
            }
            else if (this.formMode == Enum.FormMode.Update) {
                this.msgToast = "Cập nhật nguyên vật liệu";
            }
            else {
                this.msgToast = "Xóa nguyên vật liệu";
            }
        },

        /**
         * Hàm handle các điều kiện tìm kiếm của người dùng
         * params: iconSearch - điều kiện tìm kiếm (chứa, bắt đầu bằng, kết thúc bằng ....), stringSearch - chuỗi tìm kiếm
         * Author: CVViệt ()
         * Date: 30/05/2023
         * Return: chuỗi tìm kiếm đã đc handle
         */
        handleCondition(iconSearch, stringSearch) {
            if (stringSearch != null && stringSearch != "") {
                switch (iconSearch) {
                    case "*": //Chứa chuỗi tìm kiếm
                        stringSearch = "%" + stringSearch + "%";
                        break;
                    case "=": //Bằng chuỗi tìm kiếm
                        break;
                    case "+": //Bắt đầu bằng chuỗi tìm kiếm
                        stringSearch = stringSearch + "%";
                        break;
                    case "-": //Kết thúc bằng chuỗi tìm kiếm
                        stringSearch = "%" + stringSearch;
                        break;
                }
                return stringSearch;
            }
        },

        /**
         * Hàm tìm kiếm theo các điều kiện
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        search() {
            let stringCode = this.stringCode, stringName = this.stringName, stringUnit = this.stringUnit, stringMaterialGroup = this.stringMaterialGroup, stringNote = this.stringNote;
            if (this.iconCode != null) {
                stringCode = this.handleCondition(this.iconCode, stringCode);
            }
            if (this.iconName != null) {
                stringName = this.handleCondition(this.iconName, stringName);
            }
            if (this.iconUnit != null) {
                stringUnit = this.handleCondition(this.iconUnit, stringUnit);
            }
            if (this.iconGroup != null) {
                stringMaterialGroup = this.handleCondition(this.iconGroup, stringMaterialGroup);
            }
            if (this.iconNote != null) {
                stringNote = this.handleCondition(this.iconNote, stringNote);
            }
            this.pageNumber = 1;
            this.getMaterialsData(stringCode, stringName, this.stringProperty, stringUnit, stringMaterialGroup, stringNote, this.stringStopFollow);
        },

        /**
         * Các hàm thay đổi điều kiện tìm kiếm theo các trường
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        searchConditionCode(iconCode) {
            this.iconCode = iconCode;
            this.search();
        },

        searchConditionName(iconName) {
            this.iconName = iconName;
            this.search();
        },

        searchByProperty(result) {
            this.stringProperty = result;
            this.search();
        },

        searchConditionUnit(iconUnit) {
            this.iconUnit = iconUnit;
            this.search();
        },

        searchConditionGroup(iconGroup) {
            this.iconGroup = iconGroup;
            this.search();
        },

        searchConditionNote(iconNote) {
            this.iconNote = iconNote;
            this.search();
        },


        showStopFollowDropList() {
            this.stopfollowDropList.isShowStopFollowDropList = !this.stopfollowDropList.isShowStopFollowDropList;
        },
        chooseStopFollow(value) {
            this.stopfollowDropList.stopfollowValue = value;
            if (value == "Có") {
                this.stringStopFollow = 1;
                this.valueStopFollow = true;
            }
            else {
                this.stringStopFollow = 0;
                this.valueStopFollow = false;
            }
            this.stopfollowDropList.isShowStopFollowDropList = false;
            this.search();
        },

        /**
         * Hàm đi đến trang đầu tiên
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        btnFirstPage(e) {
            try {
                if (this.pageNumber == 1) {
                    e.preventDefault();
                }
                else {
                    this.pageNumber = 1;
                    this.getMaterialsData();
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm đi đến trang trước
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        btnPrevPage(e) {
            try {
                if (this.pageNumber == 1) {
                    e.preventDefault();
                }
                else {
                    this.pageNumber -= 1;
                    this.getMaterialsData();
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm chọn trang mong muốn
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        enterPageNumber() {
            try {
                if (this.pageNumber > this.totalPages) {
                    this.pageNumber = this.totalPages
                }
                this.getMaterialsData();
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm đi đến trang tiếp
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        btnNextPage(e) {
            try {
                if (this.pageNumber == this.totalPages) {
                    e.preventDefault();
                }
                else {
                    this.pageNumber += 1;
                    this.getMaterialsData();
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm đi đến trang cuối
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        btnLastPage(e) {
            try {
                if (this.pageNumber == this.totalPages) {
                    e.preventDefault();
                }
                else {
                    this.pageNumber = this.totalPages;
                    this.getMaterialsData();
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm chọn số bản ghi/trang
         * params: ()
         * Author: CVViệt ()
         * Date: 30/05/2023
         */
        showRecordDropList() {
            this.isShowRecordDropList = !this.isShowRecordDropList;
        },
        chooseNumberRecords(number) {
            this.pageSize = number;
            this.isShowRecordDropList = false;
            this.getMaterialsData();
        },
    }
}
</script>
<style scoped>
#material-container {
    color: #000;
    line-height: 17px;
    height: 100%;
}

.content-title {
    font-size: 28px;
}

.sub-title {
    color: #0072bc;
    font-size: 18px;
}

.operations {
    background-color: #f2f2f2;
    border: 1px solid #bbb;
    border-bottom: unset !important;
    padding: 2px;
}

.search-option-container {
    position: relative;
}

/* .search-option {
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
} */

.row-active {
    background-color: #acd4ef;
}

.notFoundMaterial {
    width: 100%;
    padding-top: 12px;
    font-size: 16px;
    display: flex;
    justify-content: center;
}

.pagination {
    height: 30px;
    border: 1px solid #bbb;
    border-top: unset;
    background-color: #f2f2f2;
    display: flex;
    justify-content: space-between;
}

.pagination .page-number input {
    width: 40px;
    padding: 4px;
}

.numberRecords-container {
    width: 60px;
    background-color: #fff;
    border: 1px solid #bbb;
    margin: 2px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    position: relative;
}

.tooltip {
    position: relative;
    display: inline-block;
}

.tooltip .tooltiptext {
    visibility: hidden;
    width: max-content;
    font-size: 12px;
    background-color: black;
    color: #fff;
    text-align: center;
    border-radius: 4px;
    padding: 5px;

    /* Position the tooltip */
    position: absolute;
    top: -100%;
    left: 150%;
}

.tooltip:hover .tooltiptext {
    visibility: visible;
}


.icon-add {
    background: url('../../assets/img/IconSprite.png') no-repeat -1px -2648px;
    width: 14px;
    height: 16px;
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

.icon-import {
    background: url('../../assets/img/IconSprite.png') no-repeat -1px -2416px;
    width: 15px;
    height: 15px;
    margin-right: 4px;
}

.icon-export {
    background: url('../../assets/img/IconSprite.png') no-repeat -1px -1552px;
    width: 15px;
    height: 16px;
    margin-right: 4px;
}

.icon-help {
    background: url('../../assets/img/IconSprite.png') no-repeat 0 -1600px;
    width: 16px;
    height: 16px;
    margin-right: 4px;
}

.icon-first-page {
    background: url('../../assets/icon/btn-firstpage.svg') no-repeat -5px -6px;
    width: 14px;
    height: 12px;
    margin-left: 8px;
    margin-right: 16px;
    z-index: 2;
    cursor: pointer;
}

.icon-prev-page {
    background: url('../../assets/icon/btn-prev-page.svg') no-repeat -8px -5px;
    width: 8px;
    height: 14px;
    margin-right: 16px;
    z-index: 1;
    cursor: pointer;
}

.icon-next-page {
    background: url('../../assets/icon/btn-next-page.svg') no-repeat -8px -5px;
    width: 8px;
    height: 14px;
    margin-right: 16px;
    z-index: 3;
    cursor: pointer;
}

.icon-last-page {
    background: url('../../assets/icon/btn-lastpage.svg') no-repeat -5px -6px;
    width: 14px;
    height: 12px;
    margin-right: 16px;
    z-index: 2;
    cursor: pointer;
}

.icon-load-page {
    margin-right: 16px;
    z-index: 1;
    cursor: pointer;
}
</style>