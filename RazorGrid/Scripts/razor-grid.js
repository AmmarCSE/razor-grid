function GridActionRedirect(Url) {
    window.location = Url;
}
$(function () {
    $('[name="ToggleAll"]').change(function () {
        $(this).closest('.grid').find('[name="GridRowCheckBox"]').prop('checked', $(this).prop('checked'));
    });
});