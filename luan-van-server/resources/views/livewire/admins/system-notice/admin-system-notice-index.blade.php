<div>
    {{-- Nothing in the world is as soft and yielding as water. --}}
    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary text-center rounded p-4">
            <div class="d-flex align-items-center justify-content-between mb-4">
                <h3 class="mb-0">Quản lý thông báo hệ thống</h3>
                <a class="btn btn-outline-primary" href="{{ route("admin.system-notification.create") }}" wire:navigate>+ Thêm mới</a>
            </div>
            <div class="table-responsive">
                <table class="table text-start align-middle table-bordered table-hover mb-0">
                    <thead>
                        <tr class="text-white">
                            <th scope="col">Mã</th>
                            <th scope="col">Miêu tả</th>
                            <th scope="col">Trạng thái</th>
                            <th scope="col">Sửa</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach ($notifications as $notification)
                            <tr>
                                <td class="text-truncate" style="max-width: 25px">{{ $notification->id }}</td>
                                <td class="text-start w-50">
                                    {{ $notification->content }}
                                </td>
                                <td style="max-width: 25px">
                                    @if ($notification->can_use)
                                        <div class="text-success">
                                            Đang hiển thị
                                        </div>
                                    @else
                                        <div class="text-danger">
                                            Không được dùng
                                        </div>
                                    @endif
                                </td>
                                <td class="text-center" style="max-width: 25px"><a class="btn btn-sm btn-primary" href="{{ route('admin.system-notification.show', ['notification_id' => $notification->id]) }}">Sửa</a></td>
                            </tr>
                        @endforeach
                    </tbody>
                </table>
            </div>
            <div class="mt-3">

                {{ $notifications->links() }}
            </div>
        </div>
    </div>


</div>
