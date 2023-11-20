<div>
    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary text-center rounded p-4">
            <div class="d-flex align-items-center justify-content-between mb-4">
                <h3 class="mb-0">Quản lý mẫu bài viết</h3>
                <a class="btn btn-outline-primary" href="{{ route("admin.template.create") }}" wire:navigate>+ Thêm mới</a>
            </div>

            <div class="table-responsive">
                <table class="table text-start align-middle table-bordered table-hover mb-0">
                    <thead>
                        <tr class="text-white">
                            <th scope="col">Tên</th>
                            <th scope="col">Nội dung</th>
                            <th scope="col">Trạng thái</th>
                            <th scope="col">Sửa</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach ($post_templates as $template)
                            <tr>
                                <td>{{ $template->name }}</td>
                                <td class="text-truncate" style="min-width: 50px; max-width: 350px;">{{ $template->content }}</td>
                                <td>
                                    @if ($template->can_use)
                                        <div class="text-success">
                                            Đang được dùng
                                        </div>
                                    @else
                                        <div class="text-danger">
                                            Không được dùng
                                        </div>
                                    @endif
                                </td>


                                <td class="text-center">
                                    <a class="btn btn-sm btn-primary" href="{{ route("admin.template.show", ["template_id" => $template->id]) }}">
                                        sửa
                                    </a>
                                </td>
                            </tr>
                        @endforeach
                    </tbody>
                </table>
            </div>
            <div class="mt-3">
                {{ $post_templates->links() }}
            </div>
        </div>
    </div>


</div>
