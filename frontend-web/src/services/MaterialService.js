import BaseService from "./base/BaseService";
import baseAPI from "@/resources/baseAPI";
import axios from "axios";

export default class MaterialService extends BaseService {

    constructor(controller = "Material") {
        super(controller);
    }


    getNewCode() {
        try {
            return axios.get(baseAPI + this.controller + "/newMaterialCode");
        }
        catch (error) {
            console.log(error);
        }
    }

    getPaging(pageSize = 25, pageNumber = 1, stringCode, stringName, stringProperty, stringUnit, stringMaterialGroup, stringNote, stringStopFollow) {
        try {
            let materialRequestURL = baseAPI + this.controller + "/Pagination" + "?pageSize=" + pageSize + "&pageNumber=" + pageNumber;
            if (stringCode != null) {
                materialRequestURL += "&stringCode=" + stringCode;
            }
            if (stringName != null) {
                materialRequestURL += "&stringName=" + stringName;
            }
            if (stringProperty != null) {
                materialRequestURL += "&stringProperty=" + stringProperty;
            }
            if (stringUnit != null) {
                materialRequestURL += "&stringUnit=" + stringUnit;
            }
            if (stringMaterialGroup != null) {
                materialRequestURL += "&stringMaterialGroup=" + stringMaterialGroup;
            }
            if (stringNote != null) {
                materialRequestURL += "&stringNote=" + stringNote;
            }
            if (stringStopFollow != null) {
                materialRequestURL += "&stringStopFollow=" + stringStopFollow;
            }
            return axios.get(materialRequestURL);
        }
        catch (error) {
            console.log(error);
        }
    }

}