import { Photo } from "../DTO/photo";
import { ActionCRUD } from "../enums/ActionCRUD";

export interface ManagePhotoRequest {
    Photo: Photo,
    Action: ActionCRUD
}