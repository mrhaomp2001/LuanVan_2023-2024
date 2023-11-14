<div>
    {{-- Nothing in the world is as soft and yielding as water. --}}
    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary text-center rounded p-4">
            <div class="d-flex align-items-center justify-content-between mb-4">
                <h3 class="mb-0">Quản lý trò chơi</h3>
                <a class="btn btn-outline-primary" href="{{ route("admin.game.create") }}" wire:navigate>+ Thêm mới</a>
            </div>
            <div class="table-responsive">
                <table class="table text-start align-middle table-bordered table-hover mb-0">
                    <thead>
                        <tr class="text-white">
                            <th scope="col">Mã</th>
                            <th scope="col">Tên trò chơi</th>
                            <th scope="col">Miêu tả</th>
                            <th scope="col">Sửa</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach ($games as $game)
                            <tr>
                                <td style="max-width: 20px;">{{ $game->id }}</td>
                                <td class="text-truncate w-25">{{ $game->name }}</td>
                                <td class="text-start w-50">
                                    {{ $game->description }}
                                </td>
                                <td class="text-center"><a class="btn btn-sm btn-primary" href="{{ route('admin.game.edit', ['game_id' => $game->id]) }}">Sửa</a></td>
                            </tr>
                        @endforeach
                    </tbody>
                </table>
            </div>

        </div>
    </div>


</div>
