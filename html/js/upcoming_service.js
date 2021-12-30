var $links = $('#sidebar-wrapper a');
$links.click(function(){
   $links.removeClass('active');
   $(this).addClass('active');
});
$('table').on('click', 'input[type="button"]', function(e){
   $(this).closest('tr').remove()
   alert('data deleted sucessfully;')
})
