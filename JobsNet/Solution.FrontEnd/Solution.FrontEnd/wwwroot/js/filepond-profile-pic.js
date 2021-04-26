FilePond.registerPlugin(
    FilePondPluginFileValidateType,
    FilePondPluginImageExifOrientation,
    FilePondPluginImagePreview,
    FilePondPluginImageCrop,
    FilePondPluginImageResize,
    FilePondPluginImageTransform,
    FilePondPluginImageEdit
);

FilePond.setOptions(es_es);

FilePond.create(
    document.querySelector('input[type ="file"]'),
    {
        labelIdle: `Arrastra una foto aquí o <span class="filepond--label-action">Examinar</span>`,
        imagePreviewHeight: 200,
        imageCropAspectRatio: '1:1',
        imageResizeTargetWidth: 200,
        imageResizeTargetHeight: 200,
        stylePanelLayout: 'compact circle',
        styleLoadIndicatorPosition: 'center bottom',
        styleButtonRemoveItemPosition: 'center bottom'
    }
);

FilePond.setOptions({
    server: {
        url: 'https://localhost:5001',
        process:(fieldName, file, metadata, load, error, progress, abort) => {
            const formData = new FormData();
            formData.append(fieldName, file, file.name);
            formData.append('UserName', $('#user-name').val());
            
            const request = new XMLHttpRequest();
            request.open('POST', '/FotosPerfil/Create');
            // Setting computable to false switches the loading indicator to infinite mode
            request.upload.onprogress = (e) => {
                progress(e.lengthComputable, e.loaded, e.total);
            };

            request.onload = function () {
            if (request.status >= 200 && request.status < 300) {
                load(request.responseText);// the load method accepts either a string (id) or an object
            }
            else {
                error('Error during Upload!');
            }
            };

            request.send(formData);
            //expose an abort method so the request can be cancelled
            return {
                abort: () => {
                    // This function is entered if the user has tapped the cancel button
                    request.abort();
                    // Let FilePond know the request has been cancelled
                    abort();
                }
            };
        }, // we've not implemented these endpoints yet, so leave them null!
        fetch: null, 
        revert: "/FotosPerfil/Revert/",
        remove: null
    }
});

function RemoveProfilePicture(guid) {
    console.log(guid);
    $.post("../FotosPerfil/RemoveProfilePicture", {guid: guid}, function(data, status){
        //alert("Data: " + data + "\nStatus: " + status);
        console.log(data);
        console.log(status);
        if(status === 'success' && data.response === 'deleted'){
            window.location.href = 'Index?Message=UpdateOferenteSuccess';
        }
    });
}
