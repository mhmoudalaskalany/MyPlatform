export interface AttachmentDto {
    id: string;
    name: string;
    fileSize: string;
    contentType: string;
    documentType: string;
    isPublic: boolean;
}

export interface AddAttachmentDto {
    id: string;
    name: string;
    fileSize: string;
    contentType: string;
    documentType: string;
    isPublic: boolean;
}

export interface UpdateAttachmentDto {
    id: string;
    name: string;
    fileSize: string;
    contentType: string;
    documentType: string;
    isPublic: boolean;
}
