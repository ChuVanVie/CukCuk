import BaseService from "./base/BaseService";
import baseAPI from "@/resources/baseAPI";
import axios from "axios";

export default class CalculationUnitService extends BaseService {

    constructor(controller = "CalculationUnit") {
        super(controller);
    }

    getPaging(pageSize = 25, pageNumber = 1, stringName, stringNote) {
        try {
            let calculationUnitRequestURL = baseAPI + this.controller + "/Pagination" + "?pageSize=" + pageSize + "&pageNumber=" + pageNumber;
            if (stringName != null) {
                calculationUnitRequestURL += "&stringName=" + stringName;
            }
            if (stringNote != null) {
                calculationUnitRequestURL += "&stringNote=" + stringNote;
            }
            return axios.get(calculationUnitRequestURL);
        } catch (error) {
            console.log(error);
        }
    }

}